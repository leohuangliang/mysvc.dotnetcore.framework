using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.Framework.IS4.Domain.ApiResourceAggregate;

namespace MySvc.Framework.IS4.MongoDB.Repository
{
    public class ApiResourceRepository : MongoDBRepository<ApiResource>, IApiResourceRepository
    {
        public ApiResourceRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
