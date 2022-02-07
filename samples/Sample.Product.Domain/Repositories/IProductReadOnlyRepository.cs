using MySvc.Framework.Domain.Core;

namespace Sample.Product.Domain.Repositories
{
    /// <summary>
    /// 产品信息，只读仓储接口
    /// </summary>
    public interface IProductReadOnlyRepository : IReadOnlyRepository<AggregatesModel.ProductAggregate.Product>
    {
    }
}
