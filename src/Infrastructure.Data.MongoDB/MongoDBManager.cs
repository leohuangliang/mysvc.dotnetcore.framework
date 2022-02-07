using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Domain.Core.Attributes;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.Framework.Infrastructure.Crosscutting.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MySvc.Framework.Infrastructure.Data.MongoDB
{
    public class MongoDBManager
    {
        public MongoDBManager(IOptions<MongoDBSettings> mongoDBSettingsAccessor)
        {
            var mongoDbSettings = mongoDBSettingsAccessor.Value;
            Client = new MongoClient(mongoDbSettings.ConnectionString);
            Database = Client.GetDatabase(mongoDbSettings.Database);
            Session = Client.StartSession();
        }

        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }
        public IClientSessionHandle Session { get; private set; }

        public IMongoCollection<TAggregateRoot> GetCollection<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            return Database.GetCollection<TAggregateRoot>(GetAttributeCollectionName(typeof(TAggregateRoot)) ?? this.Pluralize(typeof(TAggregateRoot)));
        }


        public void CreateCollections(List<string> collectionNames)
        {
            foreach (var name in collectionNames)
            {
                var filter = new BsonDocument("name", name);
                var collections = Database.ListCollections(new ListCollectionsOptions() { Filter = filter });
                bool exists = collections.Any();
                if (!exists)
                {
                    Database.CreateCollection(name);
                }
            }
        }

        public void CreateCollections()
        {
            var collectionNames = GetAllCollectionNames();
            foreach (var name in collectionNames)
            {
                var filter = new BsonDocument("name", name);
                var collections = Database.ListCollections(new ListCollectionsOptions() { Filter = filter });
                bool exists = collections.Any();
                if (!exists)
                {
                    Database.CreateCollection(name);
                }
            }
        }

        public void CreateCollectionsByAssemblyNames(List<string> assemblyNames)
        {
            var collectionNames = GetAllCollectionNames(assemblyNames);
            foreach (var name in collectionNames)
            {
                var filter = new BsonDocument("name", name);
                var collections = Database.ListCollections(new ListCollectionsOptions() { Filter = filter });
                bool exists = collections.Any();
                if (!exists)
                {
                    Database.CreateCollection(name);
                }
            }
        }

        public List<string> GetAllCollectionNames(List<string> assemblyNames = null)
        {
            List<string> names = new List<string>();
            List<Assembly> assList = new List<Assembly>();
            if (assemblyNames == null || assemblyNames.Count == 0)
            {
                assList = AppDomain.CurrentDomain.GetAssemblies().ToList();

            }
            else
            {
                foreach (var assemblyName in assemblyNames)
                {
                    var ass = Assembly.Load(assemblyName);
                    if (ass != null)
                    {
                        assList.Add(ass);
                    }
                }
            }


            foreach (var ass in assList)
            {
                var aggs = ass.GetTypes()
                    .Where(c => c.IsPublic && c.IsClass && !c.IsAbstract && c.GetInterfaces().Contains(typeof(IAggregateRoot))).ToList();
                if (aggs.Any())
                {
                    foreach (var item in aggs)
                    {
                        var name = (item.GetTypeInfo()
                            .GetCustomAttributes(typeof(AggregateRootNameAttribute))
                            .FirstOrDefault() as AggregateRootNameAttribute)?.Name;

                        if (!string.IsNullOrWhiteSpace(name)) names.Add(name);
                        else names.Add(this.Pluralize(item));
                    }
                }
            }


            return names;
        }
        /// <summary>
        /// 根据类型名转化成复数
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <returns></returns>
        private string Pluralize(Type type)
        {
            return (type.Name.Pluralize()).Camelize();
        }

        /// <summary>
        /// 返回集合名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetAttributeCollectionName(Type t)
        {
            return (t.GetTypeInfo()
                                     .GetCustomAttributes(typeof(AggregateRootNameAttribute))
                                     .FirstOrDefault() as AggregateRootNameAttribute)?.Name;
        }
    }
}
