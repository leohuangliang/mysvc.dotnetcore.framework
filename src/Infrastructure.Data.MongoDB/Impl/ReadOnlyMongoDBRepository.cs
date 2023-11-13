using MongoDB.Driver;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Domain.Core.Paged;
using MySvc.Framework.Domain.Core.Specification;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Data.MongoDB.Impl
{
    /// <summary>
    /// 只读类型的mongodb仓储实现
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class ReadOnlyMongoDBRepository<TAggregateRoot> : IReadOnlyRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        protected readonly IMongoDBContext _mongoDBContext;

        /// <summary>
        /// 是否基于会话Session(也可以认为是否需要事务), 如果是只读查询，一般可以不需要
        /// </summary>
        protected readonly bool _isBaseOnSession;

        /// <summary>
        /// </summary>
        public ReadOnlyMongoDBRepository(IMongoDBContext context, bool isBaseOnSession = false)
        {
            _mongoDBContext = context;
            _isBaseOnSession = isBaseOnSession;
        }

        public IDBContext Context => _mongoDBContext;

        /// <summary>
        /// 根据聚合根的ID值，从仓储中读取聚合根。
        /// </summary>
        /// <param name="key">聚合根的ID值。</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例。</returns>
        public virtual async Task<TAggregateRoot> GetByKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();
            var filter = Builders<TAggregateRoot>.Filter.Eq(c => c.Id, key);

            if (_isBaseOnSession)
            {
                return await collection.Find(_mongoDBContext.Session, filter).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            }

            return await collection.Find(filter).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 通过条件获取聚合根
        /// </summary>
        /// <param name="specification">条件参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例。</returns>
        public virtual async Task<TAggregateRoot> GetAsync(Domain.Core.Specification.ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            if (_isBaseOnSession)
            {
                return await collection.Find(_mongoDBContext.Session, specification.GetExpression()).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            }

            return await collection.Find(specification.GetExpression()).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 获取所有聚合根列表
        /// </summary>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例列表</returns>
        public virtual Task<List<TAggregateRoot>> GetAllAsync(SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default)
        {
            return GetAllAsync<TAggregateRoot>(sortCriteriaDefinition, cancellationToken);
        }

        /// <summary>
        /// 获取所有聚合根列表, 返回指定的投影对象
        /// </summary>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根投影实例的列表</returns>
        public virtual async Task<List<TProjection>> GetAllAsync<TProjection>(SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default) where TProjection : class
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            var findOption = new FindOptions<TAggregateRoot, TProjection>();

            //投影
            if (typeof(TProjection) != typeof(TAggregateRoot))
            {
                findOption.Projection = BuildProjectionDefinition<TProjection>();
            }

            //排序
            if (sortCriteriaDefinition != null)
            {
                findOption.Sort = BuildSortDefinition(sortCriteriaDefinition);
            }

            IAsyncCursor<TProjection> asyncCursor;

            if (_isBaseOnSession)
            {
                asyncCursor = await collection.FindAsync(_mongoDBContext.Session, Builders<TAggregateRoot>.Filter.Empty, findOption, cancellationToken);
            }
            else
            {
                asyncCursor = await collection.FindAsync(Builders<TAggregateRoot>.Filter.Empty, findOption, cancellationToken);
            }

            return asyncCursor.ToList();
        }

        /// <summary>
        /// 根据指定的规约，排序字段和排序方式，查询符合条件的聚合根实体数据.
        /// 若指定了最大返回数量（参数 <paramref name="maxResultCount"/>）, 则最大返回指定数量的结果集。 如目标数据的数量有100个，但是指定只返回前20个。
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="maxResultCount">获取的最大数量限制，默认为0，表示不限制</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例列表</returns>
        public virtual Task<List<TAggregateRoot>> GetListAsync(ISpecification<TAggregateRoot> specification, SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null,
            int maxResultCount = 0, CancellationToken cancellationToken = default)
        {
            return GetListAsync<TAggregateRoot>(specification, sortCriteriaDefinition, maxResultCount, cancellationToken);
        }

        /// <summary>
        /// 根据指定的规约，排序字段和排序方式，查询符合条件的聚合根实体, 返回的其【投影】对象的列表
        /// 若指定了最大返回数量（参数 <paramref name="maxResultCount"/>）, 则最大返回指定数量的结果集。 如目标数据的数量有100个，但是指定只返回前20个。
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="maxResultCount">获取的最大数量限制，默认为0，表示不限制</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根投影实例的列表</returns>
        public virtual async Task<List<TProjection>> GetListAsync<TProjection>(ISpecification<TAggregateRoot> specification, SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null,
            int maxResultCount = 0, CancellationToken cancellationToken = default)  where TProjection : class
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            //查询规约初始化
            specification ??= Specification<TAggregateRoot>.Eval(x => true);

            var findOption = new FindOptions<TAggregateRoot, TProjection>();

            //投影
            if (typeof(TProjection) != typeof(TAggregateRoot))
            {
                findOption.Projection = BuildProjectionDefinition<TProjection>();
            }

            //排序
            if (sortCriteriaDefinition != null)
            {
                findOption.Sort = BuildSortDefinition(sortCriteriaDefinition);
            }

            if (maxResultCount > 0)
            {
                findOption.Limit = maxResultCount;
            }

            IAsyncCursor<TProjection> asyncCursor;

            if (_isBaseOnSession)
            {
                asyncCursor = await collection.FindAsync(_mongoDBContext.Session, specification.GetExpression(), findOption, cancellationToken);
            }
            else
            {
                asyncCursor = await collection.FindAsync(specification.GetExpression(), findOption, cancellationToken);
            }

            return asyncCursor.ToList();
        }

        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有聚合根。
        /// </summary>
        /// <param name="pageNumber">页号</param>
        /// <param name="pageSize">页内行数</param>
        /// <param name="specification">查询规约</param>
        /// <param name="orderBys">组合排序</param>
        /// <returns>分页结果</returns>
        [Obsolete("过时的方法，不建议使用")]
        public virtual async Task<PagedResult<TAggregateRoot>> FindInPageAsync(
            int pageNumber,
            int pageSize,
            ISpecification<TAggregateRoot> specification,
            Dictionary<Expression<Func<TAggregateRoot, dynamic>>, SortOrder> orderBys)
        {
            ValidPageNumberAndSize(pageNumber, pageSize);

            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            //查询总数
            var totalCount = await CountAsync(specification);

            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;
            int totalPages = ((int)totalCount + pageSize - 1) / pageSize;

            specification = specification ?? Specification<TAggregateRoot>.Eval(x => true);

            //排序构建
            var sortBuilder = Builders<TAggregateRoot>.Sort;
            SortDefinition<TAggregateRoot> sortDefinition = null;

            if (orderBys != null && orderBys.Count > 0)
            {
                foreach (var item in orderBys)
                {
                    sortDefinition = item.Value == SortOrder.Descending ? sortBuilder.Descending(item.Key) : sortBuilder.Ascending(item.Key);
                }
            }

            IAsyncCursor<TAggregateRoot> asyncCursor;

            var findOptions = new FindOptions<TAggregateRoot>()
            {
                Limit = take,
                Skip = skip,
                Sort = sortDefinition
            };

            if (_isBaseOnSession)
            {
                asyncCursor = await collection.FindAsync(_mongoDBContext.Session, specification.GetExpression(), findOptions);
            }
            else
            {
                asyncCursor = await collection.FindAsync(specification.GetExpression(), findOptions);
            }

            var pageList = asyncCursor.ToList();

            return new PagedResult<TAggregateRoot>(totalCount, totalPages, pageSize, pageNumber, pageList);
        }

        #region 分页查询

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
        public virtual Task<SinglePageResult<TAggregateRoot>> FindInPageWithoutCountAsync(int pageNumber, int pageSize, 
            ISpecification<TAggregateRoot> specification, SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default)
        {
            return FindInPageWithoutCountAsync<TAggregateRoot>(pageNumber, pageSize, specification, sortCriteriaDefinition, cancellationToken);
        }

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
        public virtual async Task<SinglePageResult<TProjection>> FindInPageWithoutCountAsync<TProjection>(int pageNumber, int pageSize, 
            ISpecification<TAggregateRoot> specification, SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default)
        {
            ValidPageNumberAndSize(pageNumber, pageSize);

            //查询总数
            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            specification ??= Specification<TAggregateRoot>.Eval(x => true);

            //排序构建
            var sortDefinition = BuildSortDefinition(sortCriteriaDefinition);
            var pageList = await FindAndGetListAsync<TProjection>(skip, take, specification, sortDefinition, cancellationToken);

            return new SinglePageResult<TProjection>(pageNumber, pageSize, pageList);
        }

        /// <summary>
        /// 分页查询数据，根据指定的规约，排序字段和排序方式，查询符合条件的聚合根实体数据，同时计算数据的总数。根据指定的第几页（<paramref name="pageNumber"/>）和每一页数量（<paramref name="pageSize"/>），返回匹配区间的聚合根实体对象列表数据。
        /// </summary>
        /// <param name="pageNumber">页码，第几页</param>
        /// <param name="pageSize">页数据大小，每一页多少条数据。</param>
        /// <param name="specification">查询条件规约</param>
        /// <param name="sortCriteriaDefinition">排序条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>数据分页结果</returns>
        public virtual Task<PagedResult<TAggregateRoot>> FindInPageAsync(int pageNumber, int pageSize,
            ISpecification<TAggregateRoot> specification, SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default)
        {
            return FindInPageAsync<TAggregateRoot>(pageNumber, pageSize, specification, sortCriteriaDefinition, cancellationToken);
        }

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
        public virtual async Task<PagedResult<TProjection>> FindInPageAsync<TProjection>(int pageNumber, int pageSize,
            ISpecification<TAggregateRoot> specification, SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition = null, CancellationToken cancellationToken = default)
        {
            ValidPageNumberAndSize(pageNumber, pageSize);

            //查询总数
            var totalCount = await CountAsync(specification, cancellationToken);

            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;
            int totalPages = ((int)totalCount + pageSize - 1) / pageSize;

            specification ??= Specification<TAggregateRoot>.Eval(x => true);

            //排序构建
            var sortDefinition = BuildSortDefinition(sortCriteriaDefinition);
            var pageList = await FindAndGetListAsync<TProjection>(skip, take, specification, sortDefinition, cancellationToken);

            return new PagedResult<TProjection>(totalCount, totalPages, pageNumber, pageSize, pageList);
        }

        #endregion

        #region 计数相关的查询

        /// <summary>
        /// 返回一个<see cref="Boolean"/>值，该值表示符合指定规约条件的聚合根是否存在。
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>如果符合指定规约条件的聚合根存在，则返回true，否则返回false。</returns>
        public async Task<bool> ExistsAsync(Domain.Core.Specification.ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
        {
            Check.Argument.IsNotNull(specification, "specification");

            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            if (_isBaseOnSession)
            {
                return (await collection.Find(_mongoDBContext.Session, specification.GetExpression()).FirstOrDefaultAsync(cancellationToken: cancellationToken)) != null;
            }

            return (await collection.Find(specification.GetExpression()).FirstOrDefaultAsync(cancellationToken: cancellationToken)) != null;
        }

        /// <summary>
        /// 计算所有聚合根的数量
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>数量</returns>
        public async Task<long> CountAsync(CancellationToken cancellationToken = default)
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();
            return await collection.EstimatedDocumentCountAsync(cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 根据条件，计数相关的聚合根的数量
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根的数量</returns>
        public async Task<long> CountAsync(ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default)
        {
            if (specification == null)
            {
                return await CountAsync(cancellationToken);
            }

            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            if (_mongoDBContext.Session != null)
            {
                return await collection.CountDocumentsAsync(_mongoDBContext.Session, specification.GetExpression(), cancellationToken: cancellationToken);
            }

            return await collection.CountDocumentsAsync(specification.GetExpression(), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 根据查询条件，以及最大限制数量，计数相关的聚合根的数量
        /// </summary>
        /// <param name="specification">查询条件规约</param>
        /// <param name="maxLimit">最大限制数量（0为不限制）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根的计数结果</returns>
        public virtual async Task<CountResult> CountAsync(ISpecification<TAggregateRoot> specification, long maxLimit, CancellationToken cancellationToken = default)
        {
            long count = 0;

            if (maxLimit <= 0)
            {
                count = await this.CountAsync(specification, cancellationToken);
                return new CountResult(count, 0, false);
            }

            var countOptions = new CountOptions()
            {
                Limit = maxLimit + 1
            };

            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            if (_mongoDBContext.Session != null)
            {
                count = await collection.CountDocumentsAsync(_mongoDBContext.Session, specification.GetExpression(), countOptions, cancellationToken);
            }
            else
            {
                count = await collection.CountDocumentsAsync(specification.GetExpression(), countOptions, cancellationToken);
            }

            return new CountResult(count > maxLimit ? maxLimit : count, maxLimit, count > maxLimit);
        }

        #endregion

        #region 私有辅助方法
        
        /// <summary>
        /// 构建排序定义 SortDefinition, 基于SortCriteriaDefinition；
        /// </summary>
        /// <param name="sortCriteriaDefinition">排序条件定义</param>
        /// <returns></returns>
        protected virtual SortDefinition<TAggregateRoot> BuildSortDefinition(SortCriteriaDefinition<TAggregateRoot> sortCriteriaDefinition)
        {
            return sortCriteriaDefinition != null ? BuildSortDefinition(sortCriteriaDefinition.GetSortCriteria()) : null;
        }
        
        /// <summary>
        /// 构建排序定义 SortDefinition, 基于SortCriteria列表；
        /// </summary>
        /// <param name="sortCriteria">排序条件</param>
        /// <returns></returns>
        protected virtual SortDefinition<TAggregateRoot> BuildSortDefinition(IList<SortCriteria<TAggregateRoot>> sortCriteria)
        {
            //排序构建
            var sortBuilder = Builders<TAggregateRoot>.Sort;
            SortDefinition<TAggregateRoot> sortDefinition = null;

            if (sortCriteria is { Count: > 0 })
            {
                foreach (var item in sortCriteria)
                {
                    if (sortDefinition == null)
                    {
                        sortDefinition = item.SortOrder == SortOrder.Descending ? sortBuilder.Descending(item.SortKeySelector) : sortBuilder.Ascending(item.SortKeySelector);
                    }
                    else
                    {
                        sortDefinition = item.SortOrder == SortOrder.Descending ? sortDefinition.Descending(item.SortKeySelector) : sortDefinition.Ascending(item.SortKeySelector);
                    }
                }
            }

            return sortDefinition;
        }
        
        /// <summary>
        /// 构建排序定义 SortDefinition,  基于 Dictionary&lt;Expression&lt;Func&lt;TAggregateRoot, dynamic&gt;&gt; 
        /// </summary>
        protected virtual SortDefinition<TAggregateRoot> BuildSortDefinition(Dictionary<Expression<Func<TAggregateRoot, dynamic>>, SortOrder> orderBys)
        {
            //排序构建
            var sortBuilder = Builders<TAggregateRoot>.Sort;
            SortDefinition<TAggregateRoot> sortDefinition = null;

            if (orderBys is { Count: > 0 })
            {
                foreach (var item in orderBys)
                {
                    if (sortDefinition == null)
                    {
                        sortDefinition = item.Value == SortOrder.Descending ? sortBuilder.Descending(item.Key) : sortBuilder.Ascending(item.Key);
                    }
                    else
                    {
                        sortDefinition = item.Value == SortOrder.Descending ? sortDefinition.Descending(item.Key) : sortDefinition.Ascending(item.Key);
                    }
                }
            }

            return sortDefinition;
        }

        /// <summary>
        /// 创建投影 ProjectionDefinition
        /// </summary>
        /// <typeparam name="TProjection">投影类型</typeparam>
        private ProjectionDefinition<TAggregateRoot, TProjection> BuildProjectionDefinition<TProjection>()
        {
            ////查询投影对象的属性
            //var properties = typeof(TProjection).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            //var fieldList = new List<ProjectionDefinition<TAggregateRoot>>();

            ////遍历
            //foreach (var proeProperty in properties)
            //{
            //    fieldList.Add(Builders<TAggregateRoot>.Projection.Include(proeProperty.Name.ToString()));
            //}

            //var projection = Builders<TAggregateRoot>.Projection.Combine(fieldList);

            ////如果没有主键，需要特地排除
            //if (properties.All(x => x.Name != "Id"))
            //{
            //    projection = projection.Exclude(x => x.Id);
            //}

            //return projection;

            //通过As的方式投影
            return Builders<TAggregateRoot>.Projection.As<TProjection>();
        }

        private async Task<List<TProjection>> FindAndGetListAsync<TProjection>(int skip, int take, ISpecification<TAggregateRoot> specification, SortDefinition<TAggregateRoot> sortDefinition, CancellationToken cancellationToken = default)
        {
            IAsyncCursor<TProjection> asyncCursor;
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            var findOptions = new FindOptions<TAggregateRoot, TProjection>()
            {
                Limit = take,
                Skip = skip,
                Sort = sortDefinition,
            };

            if (typeof(TProjection) != typeof(TAggregateRoot))
            {
                findOptions.Projection = BuildProjectionDefinition<TProjection>();
            }

            if (_isBaseOnSession)
            {
                asyncCursor = await collection.FindAsync(_mongoDBContext.Session, specification.GetExpression(), findOptions, cancellationToken);
            }
            else
            {
                asyncCursor = await collection.FindAsync(specification.GetExpression(), findOptions, cancellationToken);
            }

            return asyncCursor.ToList();
        }

        protected virtual void ValidPageNumberAndSize(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            }
        }

        #endregion
    }
}
