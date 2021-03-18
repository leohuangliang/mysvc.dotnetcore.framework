using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.DotNetCore.Framework.IS4.Domain.IdentityResourceAggregate;

namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Repository
{
    public class IdentityResourceRepository : MongoDBRepository<IdentityResource>, IIdentityResourceRepository
    {
        public IdentityResourceRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
