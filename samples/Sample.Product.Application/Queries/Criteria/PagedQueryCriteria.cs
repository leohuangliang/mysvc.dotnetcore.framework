using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Product.Application.Queries.Criteria
{
    public abstract class PagedQueryCriteria
    {
        public PagedQueryCriteria()
        {
            this.PageIndex = 1;
            this.PageSize = 20;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

    }
}
