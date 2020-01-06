using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获得一个<see cref="System.Boolean"/>值,该值表述了当前的Unit Of Work事务是否已被提交。
        /// </summary>
        bool Committed { get; }
        /// <summary>
        /// 开始工作单元
        /// </summary>
        /// <returns></returns>
        void BeginTransaction();
        /// <summary>
        /// 提交当前的Unit Of Work事务。
        /// </summary>
        Task CommitAsync();
        /// <summary>
        /// 回滚当前的Unit Of Work事务。
        /// </summary>
        Task RollbackAsync();
    }
}