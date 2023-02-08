using MongoDB.Driver;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;

namespace MySvc.Framework.Infrastructure.Data.MongoDB
{
    /// <summary>
    /// This is the interface of the IMongoDbContext which is managed by the <see cref="MongoDBContext"/>.
    /// </summary>
    public interface IMongoDBContext : IDBContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
        IClientSessionHandle Session { get; }
        IMongoCollection<TAggregateRoot> GetCollection<TAggregateRoot>() where TAggregateRoot : IAggregateRoot;
    }
}