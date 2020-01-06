using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core.Paged;
using MySvc.DotNetCore.Framework.Domain.Core.Specification;

namespace MySvc.DotNetCore.Framework.Domain.Core
{
    public interface IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// 根据聚合根的ID值，从仓储中读取聚合根。
        /// </summary>
        /// <param name="key">聚合根的ID值。</param>
        /// <returns>聚合根实例。</returns>
        Task<TAggregateRoot> GetByKeyAsync(string key);

        /// <summary>
        /// 通过条件获取聚合根
        /// </summary>
        /// <param name="specification">条件参数</param>
        /// <returns>聚合根实例。</returns>
        Task<TAggregateRoot> GetAsync(Domain.Core.Specification.ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// 返回一个<see cref="Boolean"/>值，该值表示符合指定规约条件的聚合根是否存在。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>如果符合指定规约条件的聚合根存在，则返回true，否则返回false。</returns>
        Task<bool> ExistsAsync(ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// 获取所有聚合根列表
        /// </summary>
        /// <returns>聚合根实例列表</returns>
        Task<IEnumerable<TAggregateRoot>> GetAllAsync();

        /// <summary>
        /// 获取所有聚合根列表, 返回指定的投影对象
        /// </summary>
        /// <returns>聚合根投影实例的列表</returns>
        Task<IEnumerable<TProjection>> GetAllAsync<TProjection>() where TProjection : class;

        /// <summary>
        /// 通过条件获取聚合根列表
        /// </summary>
        /// <param name="specification">条件参数</param>
        /// <returns>聚合根实例列表</returns>
        Task<IEnumerable<TAggregateRoot>> GetListAsync(Domain.Core.Specification.ISpecification<TAggregateRoot> specification);

        /// <summary>
        /// 通过条件获取列表, 返回指定的投影对象
        /// </summary>
        /// <param name="specification">条件参数</param>
        /// <returns>聚合根投影实例的列表</returns>
        Task<IEnumerable<TProjection>> GetListAsync<TProjection>(Domain.Core.Specification.ISpecification<TAggregateRoot> specification) where TProjection : class;

        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有聚合根。
        /// </summary>
        /// <param name="pageNumber">页号</param>
        /// <param name="pageSize">页内行数</param>
        /// <param name="spceification">规约</param>
        /// <param name="orderBys">组合排序</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<TAggregateRoot>> FindInPageAsync(int pageNumber, int pageSize, ISpecification<TAggregateRoot> spceification,
            Dictionary<Expression<Func<TAggregateRoot, dynamic>>, SortOrder> orderBys);

        /// <summary>
        /// 获取所有聚合根的数量
        /// </summary>
        /// <returns>数量</returns>
        Task<long> CountAsync();

        /// <summary>
        /// 根据条件，计数相关的聚合根的数量
        /// </summary>
        /// <param name="specification">条件</param>
        /// <returns>条件</returns>
        Task<long> CountAsync(ISpecification<TAggregateRoot> specification);


    }
}
