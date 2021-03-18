using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.DotNetCore.Framework.IS4.Domain.ClientAggregate;

namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Repository
{
    public class ClientRepository : MongoDBRepository<Client>, IClientRepository
    {
        public ClientRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
