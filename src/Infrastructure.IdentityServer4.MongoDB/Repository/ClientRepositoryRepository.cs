using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.Framework.IS4.Domain.ClientAggregate;

namespace MySvc.Framework.IS4.MongoDB.Repository
{
    public class ClientRepository : MongoDBRepository<Client>, IClientRepository
    {
        public ClientRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
