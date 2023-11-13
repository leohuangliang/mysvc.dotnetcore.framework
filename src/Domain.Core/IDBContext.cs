using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32;
using MySvc.Framework.Infrastructure.Crosscutting;

namespace MySvc.Framework.Domain.Core
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

        #region Callback Event

        /// <summary>
        /// 注册回调方法，在DBContext提交的时候，触发相关的Action；
        /// 执行完之后，会清空所有回调(提交成功的回调和回滚的回调)。
        /// 若产生回滚，也会清空掉此注册的回调。
        /// </summary>
        /// <param name="callback"></param>
        void RegisterCallbackOnCommit(Action<IDBContext> callback);

        /// <summary>
        /// 注册回调方法，在DBContext回滚的时候，触发相关的Action；
        /// 执行完之后，会清空所有回调。并且也会清空 DBContext提交成功的回调。
        /// </summary>
        /// <param name="callback"></param>
        void RegisterCallbackOnRollback(Action<IDBContext> callback);

        #endregion
    }
}
