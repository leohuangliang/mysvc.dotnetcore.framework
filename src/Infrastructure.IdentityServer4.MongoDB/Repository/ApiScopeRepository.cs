using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.DotNetCore.Framework.IS4.Domain.ApiScopeAggregate;

namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Repository
{
    public class ApiScopeRepository : MongoDBRepository<ApiScope>, IApiScopeRepository
    {
        public ApiScopeRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
