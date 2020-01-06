namespace Sample.Product.Application.Queries.Criteria
{
    /// <summary>
    /// 产品查询条件
    /// </summary>
    public class ProductQueryCriteria : PagedQueryCriteria
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Title { get; set; }
    }
}
