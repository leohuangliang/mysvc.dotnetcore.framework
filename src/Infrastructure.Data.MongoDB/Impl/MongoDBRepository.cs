using MySvc.Framework.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Data.MongoDB.Impl
{
    /// <summary>
    /// 泛型读写仓储
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    public class MongoDBRepository<TAggregateRoot> : ReadOnlyMongoDBRepository<TAggregateRoot>, IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {

        public MongoDBRepository(IMongoDBContext context) : base(context, true)
        {
        }

        /// <summary>
        /// 将指定的聚合根添加到仓储中。
        /// </summary>
        /// <param name="aggregateRoot">需要添加到仓储的聚合根实例。</param>
        public async Task AddAsync(TAggregateRoot aggregateRoot)
        {
            await _mongoDBContext.RegisterNew(aggregateRoot);
        }

        /// <summary>
        /// 批量将指定的聚合根添加到仓储中。
        /// </summary>
        /// <param name="aggregateRoots">需要添加到仓储的聚合根实例列表。</param>
        public async Task AddAsync(IList<TAggregateRoot> aggregateRoots)
        {
            await _mongoDBContext.RegisterNew(aggregateRoots);
        }

        /// <summary>
        /// 将指定的聚合根从仓储中移除。
        /// </summary>
        /// <param name="aggregateRoot">需要从仓储中移除的聚合根。</param>
        public async Task RemoveAsync(TAggregateRoot aggregateRoot)
        {
            await _mongoDBContext.RegisterDeleted(aggregateRoot);
        }

        /// <summary>
        /// 批量将指定的聚合根从仓储中移除。
        /// </summary>
        /// <param name="aggregateRoots">需要从仓储中移除的聚合根实例列表。</param>
        public async Task RemoveAsync(IList<TAggregateRoot> aggregateRoots)
        {
            await _mongoDBContext.RegisterDeleted(aggregateRoots);
        }

        /// <summary>
        /// 更新指定的聚合根。
        /// </summary>
        /// <param name="aggregateRoot">需要更新的聚合根。</param>
        public async Task UpdateAsync(TAggregateRoot aggregateRoot)
        {
            await _mongoDBContext.RegisterModified(aggregateRoot);
        }

        /// <summary>
        /// 批量更新指定的聚合根。
        /// </summary>
        /// <param name="aggregateRoots">需要更新的聚合根实例列表。</param>
        public async Task UpdateAsync(IList<TAggregateRoot> aggregateRoots)
        {
            await _mongoDBContext.RegisterModified(aggregateRoots);
        }
    }
}
