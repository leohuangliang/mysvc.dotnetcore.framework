using System;
using System.Linq.Expressions;

namespace MySvc.Framework.Domain.Core.Paged
{
    /// <summary>
    /// 排序标准的定义
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public static class SortCriteriaDefinitionBuilder<T> where T : class, IAggregateRoot
    {
        /// <summary>
        /// 构建 根据关键字按升序对序列中的元素进行排序 的定义；
        /// </summary>
        public static SortCriteriaDefinition<T> Ascending(Expression<Func<T, dynamic>> keySelector)
        {
            return new SortCriteriaDefinition<T>(keySelector, SortOrder.Ascending);
        }

        /// <summary>
        /// 构建 根据关键字按降序对序列中的元素进行排序 的定义；
        /// </summary>
        public static SortCriteriaDefinition<T> Descending(Expression<Func<T, dynamic>> keySelector)
        {
            return new SortCriteriaDefinition<T>(keySelector, SortOrder.Descending);
        }
    }
}
