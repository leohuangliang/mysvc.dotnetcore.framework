using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.Framework.IS4.Domain.ApiScopeAggregate;

namespace MySvc.Framework.IS4.MongoDB.Repository
{
    public class ApiScopeRepository : MongoDBRepository<ApiScope>, IApiScopeRepository
    {
        public ApiScopeRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
