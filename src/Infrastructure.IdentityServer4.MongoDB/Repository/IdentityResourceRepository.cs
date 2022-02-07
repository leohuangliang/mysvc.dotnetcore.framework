using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.Framework.IS4.Domain.IdentityResourceAggregate;

namespace MySvc.Framework.IS4.MongoDB.Repository
{
    public class IdentityResourceRepository : MongoDBRepository<IdentityResource>, IIdentityResourceRepository
    {
        public IdentityResourceRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
