using System.Collections.Generic;

namespace Sample.Order.Application.Queries.Criteria
{
    /// <summary>
    /// 表示分页查询结果
    /// </summary>
    public class PagedQueryResult<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 查询记录数
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// 总的页面数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public IList<T> Data { get; set; }
    }
}
