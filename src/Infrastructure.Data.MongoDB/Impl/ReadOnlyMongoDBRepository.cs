using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Domain.Core.Paged;
using MySvc.Framework.Domain.Core.Specification;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        /// <returns>聚合根实例。</returns>
        public async Task<TAggregateRoot> GetByKeyAsync(string key)
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();
            var filter = Builders<TAggregateRoot>.Filter.Eq(c => c.Id, key);

            if (_isBaseOnSession)
            {
                return await collection.Find(_mongoDBContext.Session, filter).SingleOrDefaultAsync();
            }

            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 通过条件获取聚合根
        /// </summary>
        /// <param name="specification">条件参数</param>
        /// <returns>聚合根实例。</returns>
        public async Task<TAggregateRoot> GetAsync(Domain.Core.Specification.ISpecification<TAggregateRoot> specification)
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            if (_isBaseOnSession)
            {
                return await collection.Find(_mongoDBContext.Session, specification.GetExpression()).FirstOrDefaultAsync();
            }

            return await collection.Find(specification.GetExpression()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取所有聚合根列表
        /// </summary>
        /// <returns>聚合根实例列表</returns>
        public async Task<List<TAggregateRoot>> GetAllAsync()
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            if (_isBaseOnSession)
            {
                return await collection.Find(_mongoDBContext.Session, Builders<TAggregateRoot>.Filter.Empty).ToListAsync();
            }

            return await collection.Find(Builders<TAggregateRoot>.Filter.Empty).ToListAsync();
        }

        /// <summary>
        /// 获取所有聚合根列表, 返回指定的投影对象
        /// </summary>
        /// <returns>聚合根投影实例的列表</returns>
        public async Task<List<TProjection>> GetAllAsync<TProjection>() where TProjection : class
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            IAsyncCursor<TProjection> asyncCursor;

            if (_isBaseOnSession)
            {
                asyncCursor = await collection.FindAsync(_mongoDBContext.Session, Builders<TAggregateRoot>.Filter.Empty,
                    new FindOptions<TAggregateRoot, TProjection>()
                    {
                        Projection = CreateProjectionDefinition<TProjection>()
                    });
            }
            else
            {
                asyncCursor = await collection.FindAsync(Builders<TAggregateRoot>.Filter.Empty,
                    new FindOptions<TAggregateRoot, TProjection>()
                    {
                        Projection = CreateProjectionDefinition<TProjection>()
                    });
            }

            return asyncCursor.ToList();
        }

        /// <summary>
        /// 通过条件获取聚合根列表
        /// </summary>
        /// <param name="specification">条件参数</param>
        /// <returns>聚合根实例列表</returns>
        public async Task<List<TAggregateRoot>> GetListAsync(Domain.Core.Specification.ISpecification<TAggregateRoot> specification)
        {
            if (specification == null)
            {
                return await GetAllAsync();
            }

            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            if (_isBaseOnSession)
            {
                return await collection.Find(_mongoDBContext.Session, specification.GetExpression()).ToListAsync();
            }

            return await collection.Find(specification.GetExpression()).ToListAsync();
        }

        /// <summary>
        /// 通过条件获取列表, 返回指定的投影对象
        /// </summary>
        /// <param name="specification">条件参数</param>
        /// <returns>聚合根投影实例的列表</returns>
        public async Task<List<TProjection>> GetListAsync<TProjection>(ISpecification<TAggregateRoot> specification)
            where TProjection : class
        {
            if (specification == null)
            {
                return await GetAllAsync<TProjection>();
            }

            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            IAsyncCursor<TProjection> asyncCursor;

            if (_isBaseOnSession)
            {
                asyncCursor = await collection.FindAsync(_mongoDBContext.Session, specification.GetExpression(),
                    new FindOptions<TAggregateRoot, TProjection>()
                    {
                        Projection = CreateProjectionDefinition<TProjection>()
                    });
            }
            else
            {
                asyncCursor = await collection.FindAsync(specification.GetExpression(),
                    new FindOptions<TAggregateRoot, TProjection>()
                    {
                        Projection = CreateProjectionDefinition<TProjection>()
                    });
            }

            return asyncCursor.ToList();
        }

        /// <summary>
        /// 创建投影 ProjectionDefinition
        /// </summary>
        /// <typeparam name="TProjection">投影类型</typeparam>
        private ProjectionDefinition<TAggregateRoot, TProjection> CreateProjectionDefinition<TProjection>()
        {
            //查询投影对象的属性
            var properties = typeof(TProjection).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            var fieldList = new List<ProjectionDefinition<TAggregateRoot>>();

            //遍历
            foreach (var proeProperty in properties)
            {
                fieldList.Add(Builders<TAggregateRoot>.Projection.Include(proeProperty.Name.ToString()));
            }

            var projection = Builders<TAggregateRoot>.Projection.Combine(fieldList);

            //如果没有主键，需要特地排除
            if (properties.All(x => x.Name != "Id"))
            {
                projection = projection.Exclude(x => x.Id);
            }

            return projection;
        }

        /// <summary>
        /// 返回一个<see cref="Boolean"/>值，该值表示符合指定规约条件的聚合根是否存在。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>如果符合指定规约条件的聚合根存在，则返回true，否则返回false。</returns>
        public async Task<bool> ExistsAsync(Domain.Core.Specification.ISpecification<TAggregateRoot> specification)
        {
            Check.Argument.IsNotNull(specification, "specification");

            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            if (_isBaseOnSession)
            {
                return (await collection.Find(_mongoDBContext.Session, specification.GetExpression()).FirstOrDefaultAsync()) != null;
            }

            return (await collection.Find(specification.GetExpression()).FirstOrDefaultAsync()) != null;
        }

        /// <summary>
        /// 获取所有聚合根的数量
        /// </summary>
        /// <returns>数量</returns>
        public async Task<long> CountAsync()
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();
            return await collection.EstimatedDocumentCountAsync();
        }

        /// <summary>
        /// 根据条件，计数相关的聚合根的数量
        /// </summary>
        /// <param name="specification">条件</param>
        /// <returns>条件</returns>
        public async Task<long> CountAsync(Domain.Core.Specification.ISpecification<TAggregateRoot> specification)
        {
            var collection = _mongoDBContext.GetCollection<TAggregateRoot>();

            specification = specification ?? Specification<TAggregateRoot>.Eval(x => true);

            if (_isBaseOnSession)
            {
                return await collection.CountDocumentsAsync(_mongoDBContext.Session, specification.GetExpression());
            }

            return await collection.CountDocumentsAsync(specification.GetExpression());
        }

        /// <summary>
        /// 根据指定的规约，以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有聚合根。
        /// </summary>
        /// <param name="pageNumber">页号</param>
        /// <param name="pageSize">页内行数</param>
        /// <param name="specification">查询规约</param>
        /// <param name="orderBys">组合排序</param>
        /// <returns>分页结果</returns>
        public async Task<PagedResult<TAggregateRoot>> FindInPageAsync(
            int pageNumber,
            int pageSize,
            ISpecification<TAggregateRoot> specification,
            Dictionary<Expression<Func<TAggregateRoot, dynamic>>, SortOrder> orderBys)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            }

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
    }
}
