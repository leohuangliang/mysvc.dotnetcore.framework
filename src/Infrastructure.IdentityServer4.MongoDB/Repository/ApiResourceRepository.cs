using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.DotNetCore.Framework.IS4.Domain.ApiResourceAggregate;

namespace MySvc.DotNetCore.Framework.IS4.MongoDB.Repository
{
    public class ApiResourceRepository : MongoDBRepository<ApiResource>, IApiResourceRepository
    {
        public ApiResourceRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
