using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting;

namespace MySvc.DotNetCore.Framework.Domain.Core
{
    /// <summary>
    /// 仓储上下文
    /// </summary>
    public interface IDBContext : IUnitOfWork, IDisposable
    {
        #region Properties
        /// <summary>
        /// 获取仓储上下文的ID。
        /// </summary>
        Guid Id { get; }
        #endregion

        #region Methods
        /// <summary> 
        /// 将指定的聚合根标注为“新建”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        Task RegisterNew<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary> 
        /// 批量将指定的聚合根标注为“新建”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="objs">需要标注状态的聚合根列表。</param>
        Task RegisterNew<TAggregateRoot>(IList<TAggregateRoot> objs)
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 将指定的聚合根标注为“更改”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        Task RegisterModified<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 批量将指定的聚合根标注为“更改”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="objs">需要标注状态的聚合根列表。</param>
        Task RegisterModified<TAggregateRoot>(IList<TAggregateRoot> objs)
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 将指定的聚合根标注为“删除”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        Task RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 批量将指定的聚合根标注为“删除”状态。
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要标注状态的聚合根类型。</typeparam>
        /// <param name="objs">需要标注状态的聚合根列表。</param>
        Task RegisterDeleted<TAggregateRoot>(IList<TAggregateRoot> objs)
            where TAggregateRoot : class, IAggregateRoot;
        #endregion
    }
}
