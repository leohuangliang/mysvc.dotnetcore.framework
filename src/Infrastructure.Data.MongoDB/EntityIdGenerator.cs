
using MongoDB.Bson;
using MySvc.Framework.Domain.Core;

namespace MySvc.Framework.Infrastructure.Data.MongoDB
{
    public class EntityIdGenerator : IEntityIdGenerator
    {
        public string GenerateId()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
