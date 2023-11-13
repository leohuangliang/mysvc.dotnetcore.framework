using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Domain.Core.Attributes;
using MySvc.Framework.Domain.Core.DomainEvents;
using MySvc.Framework.Domain.Core.Impl;
using MySvc.Framework.Infrastructure.Crosscutting.Exceptions;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.Framework.Infrastructure.Crosscutting.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Data.MongoDB.Impl
{
    /// <summary>
    /// 基于MongoDB 的是数据库上下文
    /// </summary>
    public class MongoDBContext : DBContext, IMongoDBContext
    {
        private readonly IEntityIdGenerator _entityIdGenerator;
        private readonly IMediator _mediator;

        private readonly List<Action<IDBContext>> _callbackOnCommitList = new List<Action<IDBContext>>();

        private readonly List<Action<IDBContext>> _callbackOnRollbackList = new List<Action<IDBContext>>();

        /// <summary>
        /// MongoDBContext Logger
        /// </summary>
        private readonly ILogger<MongoDBContext> _logger;

        public MongoDBContext(IEntityIdGenerator entityIdGenerator, IOptions<MongoDBSettings> mongoDBSettingsAccessor,
            IMediator mediator, ILogger<MongoDBContext> logger) : base(entityIdGenerator)
        {
            if (mongoDBSettingsAccessor is null)
            {
                throw new ArgumentNullException(nameof(mongoDBSettingsAccessor));
            }

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _entityIdGenerator = entityIdGenerator ?? throw new ArgumentNullException(nameof(entityIdGenerator));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            var mongoDbSettings = mongoDBSettingsAccessor.Value;
            Client = new MongoClient(mongoDbSettings.ConnectionString);
            Database = Client.GetDatabase(mongoDbSettings.Database);

            Session = Client.StartSession(); 
            DisableConcurrencyControl = mongoDBSettingsAccessor.Value.DisableConcurrencyControl;
        }

        /// <summary>
        /// The IMongoClient from the official MongoDb driver
        /// </summary>
        public IMongoClient Client { get; }

        /// <summary>
        /// The IMongoDatabase from the official Mongodb driver
        /// </summary>
        public IMongoDatabase Database { get; }

        /// <summary>
        /// Mongodb Session
        /// </summary>
        public IClientSessionHandle Session { get; private set; }

        /// <summary>
        /// 是否禁用数据行的并发控制
        /// </summary>
        public bool DisableConcurrencyControl { get; private set; }

        public IMongoCollection<TAggregateRoot> GetCollection<TAggregateRoot>() where TAggregateRoot : IAggregateRoot
        {
            return Database.GetCollection<TAggregateRoot>(GetAttributeCollectionName(typeof(TAggregateRoot)) ?? this.Pluralize(typeof(TAggregateRoot)));
        }

        /// <summary> 
        /// 新建聚合根
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型。</typeparam>
        /// <param name="obj">需要新增的聚合根。</param>
        public override async Task RegisterNew<TAggregateRoot>(TAggregateRoot obj)
        {
            //try
            //{
                if (obj.IsTransient())
                {
                    obj.SetId(_entityIdGenerator.GenerateId());
                }

                if (obj.CreatedOn == DateTime.MinValue || obj.CreatedOn == DateTime.MaxValue)
                {
                    obj.CreatedOn = DateTime.Now;
                }

                if (obj.ModifiedOn == DateTime.MinValue || obj.ModifiedOn == DateTime.MaxValue)
                {
                    obj.ModifiedOn = DateTime.Now;
                }

                obj.Timestamp = DateTimeOffset.UtcNow.Ticks.ToString();

                await this.GetCollection<TAggregateRoot>().InsertOneAsync(this.Session, obj);

                //发布领域事件
                if (_mediator != null)
                {
                    await _mediator.DispatchDomainEventsAsync(obj);
                }

            //    Committed = false;
            //}
            //catch (Exception exception)
            //{
            //    if (Session.IsInTransaction)
            //    {
            //        await Session.AbortTransactionAsync();
            //        Committed = true;
            //    }

            //    if (exception is MongoException mongoException)
            //    {
            //        _logger.LogError($"Insert Error, ObjType:[${obj.GetType()}], ObjId:[{obj.Id}]" +
            //                         $", Message;[{mongoException.Message}]" +
            //                         $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");
            //    }

            //    throw;
            //}
        }

        /// <summary> 
        /// 批量新建聚合根
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要新增的聚合根类型。</typeparam>
        /// <param name="objs">需要新增的聚合根列表。</param>
        public override async Task RegisterNew<TAggregateRoot>(IList<TAggregateRoot> objs)
        {
            //try
            //{
                if (objs != null && objs.Any())
                {
                    var collection = this.GetCollection<TAggregateRoot>();

                    foreach (var obj in objs)
                    {
                        if (obj.IsTransient())
                        {
                            obj.SetId(_entityIdGenerator.GenerateId());
                        }

                        if (obj.CreatedOn == DateTime.MinValue || obj.CreatedOn == DateTime.MaxValue)
                        {
                            obj.CreatedOn = DateTime.Now;
                        }

                        if (obj.ModifiedOn == DateTime.MinValue || obj.ModifiedOn == DateTime.MaxValue)
                        {
                            obj.ModifiedOn = DateTime.Now;
                        }

                        obj.Timestamp = DateTimeOffset.UtcNow.Ticks.ToString();
                    }

                    await collection.InsertManyAsync(this.Session, objs, new InsertManyOptions());

                    //发布领域事件
                    if (_mediator != null)
                    {
                        await _mediator.DispatchDomainEventsAsync(new List<IAggregateRoot>(objs));
                    }

                    Committed = false;
                }
            //}
            //catch (Exception exception)
            //{
            //    if (Session.IsInTransaction)
            //    {
            //        await Session.AbortTransactionAsync();
            //        Committed = true;
            //    }

            //    if (exception is MongoException mongoException)
            //    {
            //        _logger.LogError($"Bulk Insert Error, ObjType:[${objs.First().GetType()}]" +
            //                         $", ObjCount: [{objs.Count}]",
            //            $", Message;[{mongoException.Message}]" +
            //            $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");

            //    }

            //    throw;
            //}
        }

        public override async Task RegisterModified<TAggregateRoot>(TAggregateRoot obj)
        {
            //try
            //{
                var collection = this.GetCollection<TAggregateRoot>();

                string originVersion = obj.Timestamp;

                var builder = Builders<TAggregateRoot>.Filter.Eq(c=> c.Id ,obj.Id);
                var filter = DisableConcurrencyControl ? builder :
                    builder & Builders<TAggregateRoot>.Filter.Eq(c => c.Timestamp, originVersion);
                obj.Timestamp = DateTimeOffset.UtcNow.Ticks.ToString();
                
                if (obj.CreatedOn == DateTime.MinValue || obj.CreatedOn == DateTime.MaxValue)
                {
                    obj.CreatedOn = DateTime.Now;
                }

                obj.ModifiedOn = DateTime.Now;
                
                var res = await collection.ReplaceOneAsync(this.Session, filter, obj, new ReplaceOptions() { IsUpsert = false} );

                if (!DisableConcurrencyControl)
                {
                    if (res == null || !res.IsAcknowledged || (res.ModifiedCount == 0 &&  await collection.CountDocumentsAsync(r=> r.Id == obj.Id) == 1))
                    {
                        throw new ConcurrencyException($"Object Type: [{obj.GetType()}], ObjectId: [{ obj.Id}], 更新时发生并发性错误, OriginVersion: {originVersion}");
                    }
                }

                //发布领域事件
                if (_mediator != null)
                {
                    await _mediator.DispatchDomainEventsAsync(obj);
                }

                Committed = false;
            //}
            //catch (Exception exception)
            //{
            //    if (Session.IsInTransaction)
            //    {
            //        await Session.AbortTransactionAsync();
            //        Committed = true;
            //    }

            //    if (exception is MongoException mongoException)
            //    {
            //        _logger.LogError($"Update Error, ObjType:[${obj.GetType()}], ObjId:[{obj.Id}]" +
            //                         $", Message;[{mongoException.Message}]" +
            //                         $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");
            //    }

            //    throw;
            //}
        }

        public override async Task RegisterModified<TAggregateRoot>(IList<TAggregateRoot> objs)
        {
            //try
            //{
                if (objs != null && objs.Any())
                {
                    var collection = this.GetCollection<TAggregateRoot>();

                    var writeModels = new List<WriteModel<TAggregateRoot>>();

                    foreach (var obj in objs)
                    {
                        string originVersion = obj.Timestamp;


                        var builder = Builders<TAggregateRoot>.Filter;
                        var filter = builder.And(builder.Eq(c => c.Id, obj.Id), builder.Eq(c => c.Timestamp, originVersion));
                        obj.Timestamp = DateTimeOffset.UtcNow.Ticks.ToString();
                        
                        if (obj.CreatedOn == DateTime.MinValue || obj.CreatedOn == DateTime.MaxValue)
                        {
                            obj.CreatedOn = DateTime.Now;
                        }

                        obj.ModifiedOn = DateTime.Now;

                        writeModels.Add(new ReplaceOneModel<TAggregateRoot>(filter, obj));
                    }

                    //批量修改
                    var bulkWriteResult = await collection.BulkWriteAsync(Session, writeModels);

                    //如果批量修改，部分成功，则认为失败，存在并发性错误（RowVersion不匹配，或者数据被删除）
                    if (!bulkWriteResult.IsAcknowledged ||  bulkWriteResult.ModifiedCount != objs.Count)
                    {
                        throw new ConcurrencyException($"批量更新类型 [{objs.First().GetType()}] 发生并发性错误");
                    }

                    //发布领域事件
                    if (_mediator != null)
                    {
                        await _mediator.DispatchDomainEventsAsync(new List<IAggregateRoot>(objs));
                    }

                    Committed = false;
                }
            //}
            //catch (Exception exception)
            //{
            //    if (Session.IsInTransaction)
            //    {
            //        await Session.AbortTransactionAsync();
            //        Committed = true;

            //        if (exception is MongoException mongoException)
            //        {
            //            _logger.LogError($"Bulk Update Error, ObjType:[${objs.First().GetType()}]" +
            //                             $", ObjCount: [{objs.Count}]",
            //                             $", Message;[{mongoException.Message}]" +
            //                             $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");

            //        }
            //    }

            //    throw;
            //}
        }

        public override async Task RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
        {
            //try
            //{
                var collection = this.GetCollection<TAggregateRoot>();
                var builder = Builders<TAggregateRoot>.Filter;
                var filter = builder.Eq(c => c.Id, obj.Id);
                await collection.DeleteOneAsync(this.Session, filter);
                //发布领域事件
                if (_mediator != null) await _mediator.DispatchDomainEventsAsync(obj);
                Committed = false;
            //}
            //catch (Exception exception)
            //{
            //    if (Session.IsInTransaction)
            //    {
            //        await Session.AbortTransactionAsync();
            //        Committed = true;
            //    }

            //    if (exception is MongoException mongoException)
            //    {
            //        _logger.LogError($"Update Error, ObjType:[${obj.GetType()}], ObjId:[{obj.Id}]" +
            //                         $", Message;[{mongoException.Message}]" +
            //                         $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");
            //    }

            //    throw;
            //}

        }

        /// <summary> 
        /// 批量删除聚合根
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型。</typeparam>
        /// <param name="objs">需要删除的聚合根列表。</param>
        public override async Task RegisterDeleted<TAggregateRoot>(IList<TAggregateRoot> objs)
        {
            //try
            //{
                if (objs != null && objs.Any())
                {
                    var collection = this.GetCollection<TAggregateRoot>();
                    var builder = Builders<TAggregateRoot>.Filter;

                    var deletedIds = objs.Select(x => x.Id).Distinct().ToList();
                    var filter = builder.In(c => c.Id, deletedIds);

                    var result = await collection.DeleteManyAsync(this.Session, filter);

                    //如果删除的数量不一致
                    if (result.DeletedCount != deletedIds.Count)
                    {
                        throw new Exception($"批量删除 {objs.First().GetType()} 失败，预期删除数量为{deletedIds.Count}, 实际删除数量为 {result.DeletedCount}");
                    }

                    //发布领域事件
                    if (_mediator != null)
                    {
                        await _mediator.DispatchDomainEventsAsync(new List<IAggregateRoot>(objs));
                    }

                    Committed = false;
                }
            //}
            //catch (Exception exception)
            //{
            //    if (Session.IsInTransaction)
            //    {
            //        await Session.AbortTransactionAsync();
            //        Committed = true;
            //    }

            //    if (exception is MongoException mongoException)
            //    {
            //        _logger.LogError($"Bulk Delete Error, ObjType:[${objs.First().GetType()}]" +
            //                         $", ObjCount: [{objs.Count}]",
            //            $", Message;[{mongoException.Message}]" +
            //            $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");

            //    }

            //    throw;
            //}
        }

        public override void BeginTransaction()
        {
            
            
            if (!Session.IsInTransaction)
            {
                Session.StartTransaction(new TransactionOptions(
                    ReadConcern.Majority,
                    ReadPreference.Primary,
                    WriteConcern.WMajority));

                Committed = false;
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public override async Task CommitAsync()
        {
            if (!Committed)
            {
                if (Session.IsInTransaction)
                {
                    //事务提交若出现短暂问题，重试3次
                    var count = 0;
                    while (true)
                    {
                        try
                        {
                            count = count + 1;
                            await Session.CommitTransactionAsync();
                            break;
                        }
                        catch (MongoException exception)
                        {
                            // can retry commit, UnknownTransactionCommitResult, retrying commit operation
                            if (exception.HasErrorLabel("UnknownTransactionCommitResult") || exception.HasErrorLabel("TransientTransactionError"))
                            {
                                if (count < 3)
                                {
                                    Thread.Sleep(100);
                                    continue;
                                }

                                throw;
                            }

                            throw;
                        }
                    }
                }

                Committed = true;

                //触发回调
                _callbackOnCommitList.ForEach(x =>
                {
                    x.Invoke(this);
                });

                _callbackOnCommitList.Clear();
                _callbackOnRollbackList.Clear();
            }
        }

        public override async Task RollbackAsync()
        {
            //如果在事务中，回滚。
            if (Session.IsInTransaction)
            {
                await Session.AbortTransactionAsync();
            }

            this.Committed = false;

            //触发回调
            _callbackOnRollbackList.ForEach(x =>
            {
                x.Invoke(this);
            });

            _callbackOnCommitList.Clear();
            _callbackOnRollbackList.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Session != null)
            {
                this.Session.Dispose();
            }
        }


        #region Callback Event

        /// <summary>
        /// 注册回调方法，在DBContext提交的时候，触发相关的Action；
        /// 执行完之后，会清空所有回调。
        /// 若产生回滚，也会清空掉此注册的回调。
        /// </summary>
        /// <param name="callback"></param>
        public override void RegisterCallbackOnCommit(Action<IDBContext> callback)
        {
            _callbackOnCommitList.Add(callback);
        }

        /// <summary>
        /// 注册回调方法，在DBContext回滚的时候，触发相关的Action；
        /// 执行完之后，会清空所有回调。并且也会清空 DBContext提交成功的回调。
        /// </summary>
        /// <param name="callback"></param>
        public override void RegisterCallbackOnRollback(Action<IDBContext> callback)
        {
            _callbackOnRollbackList.Add(callback);
        }

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Registers the MongoDB Bson serialization conventions.
        /// </summary>

        public static void RegisterConventions()
        {
            ConventionRegistryHelper.ReplaceDefaultConventionPack();


            //if (additionConventions != null)
            //    conventionPack.AddRange(additionConventions);

            //ConventionRegistry.Register("DefaultConvention", conventionPack, t => true);

            //BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            //BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

        }

        #endregion

        /// <summary>
        /// 根据类型名转化成复数
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <returns></returns>
        private string Pluralize(Type type)
        {
            return (type.Name.Pluralize()).Camelize();
        }


        /// <summary>
        /// 返回集合名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetAttributeCollectionName(Type t)
        {
            return (t.GetTypeInfo()
                                     .GetCustomAttributes(typeof(AggregateRootNameAttribute))
                                     .FirstOrDefault() as AggregateRootNameAttribute)?.Name;
        }

    }
}
