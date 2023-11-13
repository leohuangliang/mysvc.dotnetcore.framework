using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MySvc.Framework.Domain.Core.Paged
{
    /// <summary>
    /// 排序标准的定义
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class SortCriteriaDefinition<T> where T : class, IAggregateRoot
    {
        private readonly IList<SortCriteria<T>> _criteria;
 
        /// <summary>
        /// </summary> 
        public SortCriteriaDefinition(Expression<Func<T, dynamic>> keySelector, SortOrder sortOrder)
        {
            _criteria = new List<SortCriteria<T>>
            {
                new SortCriteria<T>(keySelector, SortOrder.Ascending)
            };
        }
        
        /// <summary>
        /// 构建增加 根据字段按升序对序列中的元素进行排序；
        /// </summary>
        public SortCriteriaDefinition<T> Ascending(Expression<Func<T, dynamic>> keySelector)
        {
            _criteria.Add(new SortCriteria<T>(keySelector, SortOrder.Ascending));
            return this;
        }

        /// <summary>
        /// 构建增加 根据字段按降序对序列中的元素进行排序；
        /// </summary>
        public SortCriteriaDefinition<T> Descending(Expression<Func<T, dynamic>> keySelector)
        {
            _criteria.Add(new SortCriteria<T>(keySelector, SortOrder.Descending));
            return this;
        }

        /// <summary>
        /// 返回排序标准的列表
        /// </summary>
        public IList<SortCriteria<T>> GetSortCriteria()
        {
            return _criteria?.ToList() ?? new List<SortCriteria<T>>();
        }
    }
}
