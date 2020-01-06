using System.Threading.Tasks;
using Sample.Product.Application.Queries.Criteria;

namespace Sample.Product.Application.Queries
{
    /// <summary>
    /// 产品信息查询器
    /// </summary>
    public interface IProductQueries
    {
        /// <summary>
        /// 根据SKU获取产品信息
        /// </summary>
        /// <param name="sku">sku信息</param>
        /// <returns>产品信息</returns>
        Task<ViewModels.Product> GetProductBySku(string sku);

        /// <summary>
        /// 分页查询产品信息
        /// </summary>
        /// <param name="criteria">查询条件</param>
        Task<PagedQueryResult<ViewModels.Product>> GetPagedList(ProductQueryCriteria criteria);
    }
}
