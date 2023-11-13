using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MySvc.Framework.Domain.Core.Paged;
using MySvc.Framework.Domain.Core.Specification;

namespace MySvc.Framework.Domain.Core
{
    /// <summary>
    /// 只读仓储接口
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public interface IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        /// <summary>
        /// 根据聚合根的ID值，从仓储中读取聚合根。
        /// </summary>
        /// <param name="key">聚合根的ID值。</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例。</returns>
        Task<TAggregateRoot> GetByKeyAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 通过条件获取聚合根
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例。</returns>
        Task<TAggregateRoot> GetAsync(Domain.Core.Specification.ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取所有聚合根列表
        /// </summary>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例列表</returns>
        Task<List<TAggregateRoot>> GetAllAsync(SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取所有聚合根列表, 返回指定的投影对象
        /// </summary>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根投影实例的列表</returns>
        Task<List<TProjection>> GetAllAsync<TProjection>(SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default) where TProjection : class;

        /// <summary>
        /// 根据指定的规约，排序字段和排序方式，查询符合条件的聚合根实体数据.
        /// 若指定了最大返回数量（参数 <paramref name="maxResultCount"/>）, 则最大返回指定数量的结果集。 如目标数据的数量有100个，但是指定只返回前20个。
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="maxResultCount">获取的最大数量限制，默认为0，表示不限制</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例列表</returns>
        Task<List<TAggregateRoot>> GetListAsync(ISpecification<TAggregateRoot> specification, SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, int maxResultCount = 0, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据指定的规约，排序字段和排序方式，查询符合条件的聚合根实体, 返回的其【投影】对象的列表
        /// 若指定了最大返回数量（参数 <paramref name="maxResultCount"/>）, 则最大返回指定数量的结果集。 如目标数据的数量有100个，但是指定只返回前20个。
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="maxResultCount">获取的最大数量限制，默认为0，表示不限制</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根投影实例的列表</returns>
        Task<List<TProjection>> GetListAsync<TProjection>(ISpecification<TAggregateRoot> specification, SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, int maxResultCount = 0, CancellationToken cancellationToken = default) where TProjection : class;

        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有聚合根。
        /// </summary>
        /// <param name="pageNumber">页号</param>
        /// <param name="pageSize">页内行数</param>
        /// <param name="specification">查询条件规约</param>
        /// <param name="orderBys">组合排序</param>
        /// <returns>分页结果</returns>
        [Obsolete("过时的方法，不建议使用")]
        Task<PagedResult<TAggregateRoot>> FindInPageAsync(int pageNumber, int pageSize, ISpecification<TAggregateRoot> specification,
            Dictionary<Expression<Func<TAggregateRoot, dynamic>>, SortOrder> orderBys);

        /// <summary>
        /// 分页查询数据，根据指定的规约，排序字段和排序方式，查询符合条件的聚合根实体数据。
        /// 根据指定的第几页（<paramref name="pageNumber"/>）和每一页数量（<paramref name="pageSize"/>），返回匹配区间的聚合根实体对象列表数据。
        /// 该方法不会同时计算总数。
        /// </summary>
        /// <param name="pageNumber">页号</param>
        /// <param name="pageSize">页内行数</param>
        /// <param name="specification">规约</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>分页结果</returns>
        Task<SinglePageResult<TAggregateRoot>> FindInPageWithoutCountAsync(int pageNumber, int pageSize, ISpecification<TAggregateRoot> specification,
            SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询数据，根据指定的规约，排序字段和排序方式，查询符合条件的聚合根实体数据。
        /// 根据指定的第几页（<paramref name="pageNumber"/>）和每一页数量（<paramref name="pageSize"/>），返回匹配区间的聚合根实体的【投影】对象列表数据。
        /// 该方法不会同时计算总数。
        /// </summary>
        /// <param name="pageNumber">页号</param>
        /// <param name="pageSize">页内行数</param>
        /// <param name="specification">规约</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>分页结果</returns>
        Task<SinglePageResult<TProjection>> FindInPageWithoutCountAsync<TProjection>(int pageNumber, int pageSize, ISpecification<TAggregateRoot> specification,
            SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询数据，根据指定的规约，排序字段和排序方式，查询符合条件的聚合根实体数据，同时计算数据的总数。根据指定的第几页（<paramref name="pageNumber"/>）和每一页数量（<paramref name="pageSize"/>），返回匹配区间的聚合根实体对象列表数据。
        /// </summary>
        /// <param name="pageNumber">页码，第几页</param>
        /// <param name="pageSize">页数据大小，每一页多少条数据。</param>
        /// <param name="specification">查询条件规约</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>数据分页结果</returns>
        Task<PagedResult<TAggregateRoot>> FindInPageAsync(int pageNumber, int pageSize, ISpecification<TAggregateRoot> specification,
            SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询数据，根据指定的规约，排序字段和排序方式，查询符合条件的聚合根实体数据，同时计算数据的总数。根据指定的第几页（<paramref name="pageNumber"/>）和每一页数量（<paramref name="pageSize"/>），返回匹配区间的聚合根实体的【投影】对象列表数据。
        /// </summary>
        /// <typeparam name="TProjection">投影类的类型</typeparam>
        /// <param name="pageNumber">页码，第几页</param>
        /// <param name="pageSize">页数据大小，每一页多少条数据。</param>
        /// <param name="specification">查询条件规约</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>数据分页结果</returns>
        Task<PagedResult<TProjection>> FindInPageAsync<TProjection>(int pageNumber, int pageSize, ISpecification<TAggregateRoot> specification,
            SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 根据指定的规约，排序字段和排序方式，同时基于上一个查询到的<see cref="TAggregateRoot"/>对象，向后再查询<paramref name="pageSize"/>个，符合条件的聚合根实体对象列表数据。
        /// </summary>
        /// <param name="specification">查询条件</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="lastAggregateEntity">上一个<see cref="TAggregateRoot"/>对象信息</param>
        /// <param name="pageSize">获取的数量</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns><see cref="TAggregateRoot"/>对象信息列表</returns>
        Task<List<TAggregateRoot>> FindAfterAsync(int pageSize, ISpecification<TAggregateRoot> specification, TAggregateRoot lastAggregateEntity, 
            SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据指定的规约，排序字段和排序方式，同时基于上一个查询到的<see cref="TAggregateRoot"/>对象，向后再查询<paramref name="pageSize"/>个，符合条件的聚合根实体的【投影】对象列表数据。
        /// </summary>
        /// <param name="specification">查询条件</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="lastAggregateEntity">上一个<see cref="TAggregateRoot"/>对象信息</param>
        /// <param name="pageSize">获取的数量</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns><see cref="TAggregateRoot"/>对象信息列表</returns>
        Task<List<TProjection>> FindAfterAsync<TProjection>(int pageSize, ISpecification<TAggregateRoot> specification, TAggregateRoot lastAggregateEntity,
            SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 计算所有聚合根的数量
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>数量</returns>
        Task<long> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件，计数相关的聚合根的数量
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根的数量</returns>
        Task<long> CountAsync(ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据查询条件，以及最大限制数量，计数相关的聚合根的数量
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="maxLimit">最大限制数量（0为不限制）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根的计数结果</returns>
        Task<CountResult> CountAsync(ISpecification<TAggregateRoot> specification, long maxLimit, CancellationToken cancellationToken = default);

        /// <summary>
        /// 返回一个<see cref="Boolean"/>值，该值表示符合指定规约条件的聚合根是否存在。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>如果符合指定规约条件的聚合根存在，则返回true，否则返回false。</returns>
        Task<bool> ExistsAsync(ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default);
    }
}
