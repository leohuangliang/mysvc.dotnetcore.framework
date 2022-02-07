using MongoDB.Driver;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.Framework.IS4.Domain.PersistedGrantAggregate;
using System;
using System.Threading.Tasks;

namespace MySvc.Framework.IS4.MongoDB.Repository
{
    public class PersistedGrantRepository : MongoDBRepository<PersistedGrant>, IPersistedGrantRepository
    {
        public PersistedGrantRepository(IMongoDBContext context) : base(context)
        {
        }

        public async Task RemoveExpired()
        {
            try
            {
                var collection = _mongoDBContext.GetCollection<PersistedGrant>();
                var builder = Builders<PersistedGrant>.Filter;
                var filter = builder.Lt(c => c.Expiration, DateTime.UtcNow);
                await collection.DeleteManyAsync(_mongoDBContext.Session, filter);
            }
            catch (Exception)
            {
                if (_mongoDBContext.Session.IsInTransaction)
                {
                    await _mongoDBContext.Session.AbortTransactionAsync();
                }

                throw;
            }
        }
    }
}
