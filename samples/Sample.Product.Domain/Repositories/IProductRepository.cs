using MySvc.DotNetCore.Framework.Domain.Core;

namespace Sample.Product.Domain.Repositories
{
    /// <summary>
    /// 产品仓储接口
    /// </summary>
    public interface IProductRepository : IRepository<AggregatesModel.ProductAggregate.Product>
    {
    }
}
