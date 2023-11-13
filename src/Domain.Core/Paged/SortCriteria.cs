using System;
using System.Linq.Expressions;

namespace MySvc.Framework.Domain.Core.Paged
{
    /// <summary>
    /// 表示一个排序标准
    /// </summary>
    public class SortCriteria<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// </summary>
        public SortCriteria(Expression<Func<TAggregateRoot, dynamic>> sortKeySelector, SortOrder sortOrder)
        {
            SortKeySelector = sortKeySelector;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// 提取排序键的委托函数
        /// </summary>
        public Expression<Func<TAggregateRoot, dynamic>> SortKeySelector { get; private set; }

        /// <summary>
        /// 排序顺序(升序/降序)
        /// </summary>
        public SortOrder SortOrder { get; private set; }
    }
}
