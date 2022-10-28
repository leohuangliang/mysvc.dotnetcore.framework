using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using Microsoft.Extensions.Options;
using Xunit;
using MongoDB.Driver;
using Xunit.Abstractions;
using MySvc.Framework.Infrastructure.Crosscutting.Options;

namespace MongodbTransaction.Tests
{
    public class AcidTest
    {
         private readonly string _connectionString = "mongodb://admin:admin123456@127.0.0.1:27017,127.0.0.1:27018,127.0.0.1:27019/?connect=replicaSet";
        //private readonly string _connectionString = "mongodb://testdb_admin:testdb_admin@172.96.207.204:27017,172.96.207.204:27018,172.96.207.204:27019/?replicaSet=capmarvelRepSet";
        private readonly string _dbName = "testdb";
        private readonly IOptions<MongoDBSettings> _options;

        private readonly ITestOutputHelper _output;
        public AcidTest(ITestOutputHelper output)
        {
            _options = Options.Create<MongoDBSettings>(new MongoDBSettings()
            {
                ConnectionString = _connectionString,
                Database = _dbName
            });
            _output = output;

            new MongoDBManager(_options).CreateCollections(new List<string> { "users", "products" });
        }

        [Fact]
        public void Single_Insert()
        {
            IMongoClient client = new MongoClient(_connectionString);

            var database = client.GetDatabase(_dbName);
            using (IClientSession session = client.StartSession())
            {
                session.StartTransaction();
                var collection = database.GetCollection<User>("users");
                var user = new User() { Name = "test" };
                try
                {
                    collection.InsertOne(user);

                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    throw;
                }

                var findUser = collection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                Assert.NotNull(findUser);
                Assert.Equal(user.Name, findUser.Name);
                _output.WriteLine("OK");
            }
        }

        [Fact]
        public void Single_Insert_Product()
        {
            IMongoClient client = new MongoClient(_connectionString);

            var database = client.GetDatabase(_dbName);
            using (IClientSession session = client.StartSession())
            {
                session.StartTransaction();
                var collection = database.GetCollection<Product>("products");
                var product = new Product() { Name = "test" , Catalog = new Catalog("a","b")};
                try
                {
                    collection.InsertOne(product);

                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    throw;
                }

                var find = collection.AsQueryable().FirstOrDefault(c => c.Id == product.Id);
                Assert.NotNull(find);
                Assert.Equal(product.Name, find.Name);

                Assert.NotNull(find.Catalog);
                Assert.Equal(product.Catalog.FirstName, find.Catalog.FirstName);
                Assert.Equal(product.Catalog.LastName, find.Catalog.LastName);
                Assert.Equal(product.Catalog, find.Catalog);
                _output.WriteLine("OK");
            }
        }

        [Fact]
        public void Insert_Multi()
        {
            IMongoClient client = new MongoClient(_connectionString);

            var database = client.GetDatabase(_dbName);
            using (var session = client.StartSession())
            {
                session.StartTransaction();
                var collection = database.GetCollection<User>("users");
                var user1 = new User() { Name = "test1" };
                var user2 = new User() { Name = "test2" };
                try
                {
                    collection.InsertOne(session, user1);
                    collection.InsertOne(session, user2);

                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    throw;
                }

                var findUser1 = collection.AsQueryable().FirstOrDefault(c => c.Id == user1.Id);
                Assert.NotNull(findUser1);
                Assert.Equal(user1.Name, findUser1.Name);

                var findUser2 = collection.AsQueryable().FirstOrDefault(c => c.Id == user2.Id);
                Assert.NotNull(findUser2);
                Assert.Equal(user2.Name, findUser2.Name);
            }
        }

        [Fact]
        public void Update_Single()
        {
            IMongoClient client = new MongoClient(_connectionString);

            var database = client.GetDatabase(_dbName);
            using (var session = client.StartSession())
            {
                session.StartTransaction();
                var collection = database.GetCollection<User>("users");
                var user = new User() { Name = "test" };
                var user3 = new User() { Name = "test3" };
                try
                {
                    collection.InsertOne(session, user);

                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    throw;
                }

                var findUser1 = collection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                Assert.NotNull(findUser1);
                Assert.Equal("test", findUser1.Name);

                session.StartTransaction();
                try
                {
                    user.Name = "test2";
                    var filter = Builders<User>.Filter.Eq(c => c.Id, user.Id);
                    collection.ReplaceOne(session, filter, user);

                    collection.InsertOne(session, user3);

                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    throw;
                }

                var findUser = collection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                Assert.NotNull(findUser);
                Assert.Equal("test2", findUser.Name);

                findUser = collection.AsQueryable().FirstOrDefault(c => c.Id == user3.Id);
                Assert.NotNull(findUser);
                Assert.Equal("test3", findUser.Name);
            }
        }

        [Fact]
        public void Update_Concurrent_Test()
        {
            IMongoClient client = new MongoClient(_connectionString);

            var database = client.GetDatabase(_dbName);
            using (var session = client.StartSession())
            {
                session.StartTransaction();
                var collection = database.GetCollection<User>("users");
                var user = new User() { Name = "test", Version = 1 };
                try
                {
                    collection.InsertOne(session, user);
                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    throw;
                }

                var findUser1 = collection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                Assert.NotNull(findUser1);
                Assert.Equal("test", findUser1.Name);

                session.StartTransaction();
                try
                {
                    user.Name = "test2";
                    var builder = Builders<User>.Filter;

                    var filter = builder.And(builder.Eq(c => c.Id, user.Id), builder.Eq(c => c.Version, 2));
                    ReplaceOneResult replaceOneResult = collection.ReplaceOne(session, filter, user);
                    if (replaceOneResult.ModifiedCount != 1)
                    {
                        throw new Exception("x");
                    }
                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();

                }
                var findUser = collection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                Assert.NotNull(findUser);
                Assert.Equal("test", findUser.Name);

            }
        }

        [Fact]
        public void AbortTransaction_Test()
        {
            IMongoClient client = new MongoClient(_connectionString);

            var database = client.GetDatabase(_dbName);
            var user = new User() { Name = "testx" };
            var product = new Product { Name = "usb" };
            using (var session = client.StartSession())
            {
                session.StartTransaction();

                var userCollection = database.GetCollection<User>("users");
                var productCollection = database.GetCollection<Product>("products");
                try
                {
                    userCollection.InsertOne(session, user);
                    productCollection.InsertOne(session, product);
                    throw new Exception("Mock Exception");
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                }
                var findUser = userCollection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                Assert.Null(findUser);
                var findProduct = productCollection.AsQueryable().FirstOrDefault(c => c.Id == product.Id);
                Assert.Null(findProduct);
            }
        }

        [Fact]
        public void Multi_Transaction_Test()
        {
            IMongoClient client =
                new MongoClient(_connectionString);

            var database = client.GetDatabase(_dbName);
            var user = new User() { Name = "testx" };
            var product = new Product { Name = "usb" };


            using (var session = client.StartSession())
            {
                session.StartTransaction();
                var userCollection = database.GetCollection<User>("users");
                var productCollection = database.GetCollection<Product>("products");
                try
                {
                    userCollection.InsertOne(session, user);
                    productCollection.InsertOne(session, product);
                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                }

                var findUser1 = userCollection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                Assert.NotNull(findUser1);
                var findProduct1 = productCollection.AsQueryable().FirstOrDefault(c => c.Id == product.Id);
                Assert.NotNull(findProduct1);
            }
        }

        [Fact]
        public void Multi_Transaction_Test_2()
        {
            IMongoClient client =
                new MongoClient(_connectionString);

            var database = client.GetDatabase(_dbName);
            var user = new User() { Name = "testx" };
            var product = new Product { Name = "usb" };


            using (var session = client.StartSession())
            {
                session.StartTransaction();
                var userCollection = database.GetCollection<User>("users");
                var productCollection = database.GetCollection<Product>("products");
                try
                {
                    userCollection.InsertOne(session, user);
                    productCollection.InsertOne(session, product);
                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                }

                var findUser1 = userCollection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                Assert.NotNull(findUser1);
                var findProduct1 = productCollection.AsQueryable().FirstOrDefault(c => c.Id == product.Id);
                Assert.NotNull(findProduct1);

                session.StartTransaction();
                var product2 = new Product { Name = "usb2" };
                try
                {
                    productCollection.InsertOne(session, product2);
                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                }

                var findProduct2 = productCollection.AsQueryable().FirstOrDefault(c => c.Id == product2.Id);
                Assert.NotNull(findProduct2);
            }


        }

        [Fact]
        public void Query_Transaction_Test()
        {
            IMongoClient client = new MongoClient(_connectionString);

            var database = client.GetDatabase(_dbName);
            var user = new User() { Name = "testx" };
            var product = new Product { Name = "usb" };
            using (var session = client.StartSession())
            {
                session.StartTransaction();

                var userCollection = database.GetCollection<User>("users");
                var productCollection = database.GetCollection<Product>("products");
                try
                {
                    userCollection.InsertOne(session, user);
                    productCollection.InsertOne(session, product);

                    var newUser = userCollection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                    Assert.NotNull(newUser);
                    throw new Exception("Mock Exception");
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                }
                var findUser = userCollection.AsQueryable().FirstOrDefault(c => c.Id == user.Id);
                Assert.Null(findUser);
                var findProduct = productCollection.AsQueryable().FirstOrDefault(c => c.Id == product.Id);
                Assert.Null(findProduct);
            }
        }


    }

    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; private set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();

        }

        public string Id { get; private set; }
        public string Name { get; set; }

        public Catalog Catalog { get; set; }
    }

    public record Catalog 
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Catalog(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
