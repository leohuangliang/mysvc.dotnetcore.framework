using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using Sample.Product.Domain.Repositories;

namespace Sample.Product.Repository.MongoDB
{
    /// <summary>
    /// 产品信息的只读查询仓储
    /// </summary>
    public class ProductReadOnlyRepository : ReadOnlyMongoDBRepository<Domain.AggregatesModel.ProductAggregate.Product>, IProductReadOnlyRepository
    {
        public ProductReadOnlyRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
