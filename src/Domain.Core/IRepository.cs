using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MySvc.Framework.Domain.Core.Paged;
using MySvc.Framework.Domain.Core.Specification;

namespace MySvc.Framework.Domain.Core
{
    /// <summary>
    /// 泛型仓储
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    public interface IRepository<TAggregateRoot> : IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        #region Methods
        /// <summary>
        /// 将指定的聚合根添加到仓储中。
        /// </summary>
        /// <param name="aggregateRoot">需要添加到仓储的聚合根实例。</param>
        Task AddAsync(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 批量将指定的聚合根添加到仓储中。
        /// </summary>
        /// <param name="aggregateRoots">需要添加到仓储的聚合根实例列表。</param>
        Task AddAsync(IList<TAggregateRoot> aggregateRoots);

        /// <summary>
        /// 将指定的聚合根从仓储中移除。
        /// </summary>
        /// <param name="aggregateRoot">需要从仓储中移除的聚合根。</param>
        Task RemoveAsync(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 批量将指定的聚合根从仓储中移除。
        /// </summary>
        /// <param name="aggregateRoots">需要从仓储中移除的聚合根实例列表。</param>
        Task RemoveAsync(IList<TAggregateRoot> aggregateRoots);

        /// <summary>
        /// 更新指定的聚合根。
        /// </summary>
        /// <param name="aggregateRoot">需要更新的聚合根。</param>
        Task UpdateAsync(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 批量更新指定的聚合根。
        /// </summary>
        /// <param name="aggregateRoots">需要更新的聚合根实例列表。</param>
        Task UpdateAsync(IList<TAggregateRoot> aggregateRoots);

        #endregion
    }
}
