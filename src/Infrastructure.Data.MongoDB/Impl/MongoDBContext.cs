using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Domain.Core.Attributes;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Exceptions;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Options;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;

namespace MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl
{
    /// <summary>
    /// 基于MongoDB 的是数据库上下文
    /// </summary>
    public class MongoDBContext : DBContext, IMongoDBContext
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// MongoDBContext Logger
        /// </summary>
        private readonly ILogger<MongoDBContext> _logger;

        public MongoDBContext(IOptions<MongoDBSettings> mongoDBSettingsAccessor,
            IMediator mediator, ILogger<MongoDBContext> logger)
        {
            if (mongoDBSettingsAccessor is null)
            {
                throw new ArgumentNullException(nameof(mongoDBSettingsAccessor));
            }

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            try
            {
                if (obj.IsTransient())
                {
                    obj.GenerateId();
                }
                obj.RowVersion = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

                await this.GetCollection<TAggregateRoot>().InsertOneAsync(this.Session, obj);

                //发布领域事件
                if (_mediator != null)
                {
                    await _mediator.DispatchDomainEventsAsync(obj);
                }

                Committed = false;
            }
            catch (Exception exception)
            {
                if (Session.IsInTransaction)
                {
                    await Session.AbortTransactionAsync();
                    Committed = true;
                }

                if (exception is MongoException mongoException)
                {
                    _logger.LogError($"Insert Error, ObjType:[${obj.GetType()}], ObjId:[{obj.Id}]" +
                                     $", Message;[{mongoException.Message}]" +
                                     $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");
                }

                throw;
            }
        }

        /// <summary> 
        /// 批量新建聚合根
        /// </summary>
        /// <typeparam name="TAggregateRoot">需要新增的聚合根类型。</typeparam>
        /// <param name="objs">需要新增的聚合根列表。</param>
        public override async Task RegisterNew<TAggregateRoot>(IList<TAggregateRoot> objs)
        {
            try
            {
                if (objs != null && objs.Any())
                {
                    var collection = this.GetCollection<TAggregateRoot>();

                    foreach (var obj in objs)
                    {
                        if (obj.IsTransient())
                        {
                            obj.GenerateId();
                        }

                        obj.RowVersion = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
                    }

                    await collection.InsertManyAsync(this.Session, objs, new InsertManyOptions());

                    //发布领域事件
                    if (_mediator != null)
                    {
                        await _mediator.DispatchDomainEventsAsync(new List<IAggregateRoot>(objs));
                    }

                    Committed = false;
                }
            }
            catch (Exception exception)
            {
                if (Session.IsInTransaction)
                {
                    await Session.AbortTransactionAsync();
                    Committed = true;
                }

                if (exception is MongoException mongoException)
                {
                    _logger.LogError($"Bulk Insert Error, ObjType:[${objs.First().GetType()}]" +
                                     $", ObjCount: [{objs.Count}]",
                        $", Message;[{mongoException.Message}]" +
                        $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");

                }

                throw;
            }
        }

        public override async Task RegisterModified<TAggregateRoot>(TAggregateRoot obj)
        {
            try
            {
                var collection = this.GetCollection<TAggregateRoot>();

                byte[] originVersion = obj.RowVersion;

                var builder = Builders<TAggregateRoot>.Filter;
                var filter = DisableConcurrencyControl ? builder.Eq(c => c.Id, obj.Id) :
                    builder.And(builder.Eq(c => c.Id, obj.Id), builder.Eq(c => c.RowVersion, originVersion));
                obj.RowVersion = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

                var res = await collection.FindOneAndReplaceAsync<TAggregateRoot>(this.Session, filter, obj,
                    new FindOneAndReplaceOptions<TAggregateRoot, TAggregateRoot>()
                    {
                        ReturnDocument = ReturnDocument.After,
                        IsUpsert = false
                    });

                if (!DisableConcurrencyControl)
                {
                    if (res == null)
                    {
                        throw new ConcurrencyException($"Object Type: [{obj.GetType()}], ObjectId: [{ obj.Id}], 更新时发生并发性错误, OriginVersion: {BitConverter.ToInt64(originVersion, 0)}");
                    }
                }

                //发布领域事件
                if (_mediator != null)
                {
                    await _mediator.DispatchDomainEventsAsync(obj);
                }

                Committed = false;
            }
            catch (Exception exception)
            {
                if (Session.IsInTransaction)
                {
                    await Session.AbortTransactionAsync();
                    Committed = true;
                }

                if (exception is MongoException mongoException)
                {
                    _logger.LogError($"Update Error, ObjType:[${obj.GetType()}], ObjId:[{obj.Id}]" +
                                     $", Message;[{mongoException.Message}]" +
                                     $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");
                }

                throw;
            }
        }

        public override async Task RegisterModified<TAggregateRoot>(IList<TAggregateRoot> objs)
        {
            try
            {
                if (objs != null && objs.Any())
                {
                    var collection = this.GetCollection<TAggregateRoot>();

                    var writeModels = new List<WriteModel<TAggregateRoot>>();

                    foreach (var obj in objs)
                    {
                        byte[] originVersion = obj.RowVersion;

                        var builder = Builders<TAggregateRoot>.Filter;
                        var filter = builder.And(builder.Eq(c => c.Id, obj.Id), builder.Eq(c => c.RowVersion, originVersion));
                        obj.RowVersion = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

                        writeModels.Add(new ReplaceOneModel<TAggregateRoot>(filter, obj));
                    }

                    //批量修改
                    var bulkWriteResult = await collection.BulkWriteAsync(Session, writeModels);

                    //如果批量修改，部分成功，则认为失败，存在并发性错误（RowVersion不匹配，或者数据被删除）
                    if (bulkWriteResult.ModifiedCount != objs.Count)
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
            }
            catch (Exception exception)
            {
                if (Session.IsInTransaction)
                {
                    await Session.AbortTransactionAsync();
                    Committed = true;

                    if (exception is MongoException mongoException)
                    {
                        _logger.LogError($"Bulk Update Error, ObjType:[${objs.First().GetType()}]" +
                                         $", ObjCount: [{objs.Count}]",
                                         $", Message;[{mongoException.Message}]" +
                                         $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");

                    }
                }

                throw;
            }
        }

        public override async Task RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
        {
            try
            {
                var collection = this.GetCollection<TAggregateRoot>();
                var builder = Builders<TAggregateRoot>.Filter;
                var filter = builder.Eq(c => c.Id, obj.Id);
                await collection.DeleteOneAsync(this.Session, filter);
                //发布领域事件
                if (_mediator != null) await _mediator.DispatchDomainEventsAsync(obj);
                Committed = false;
            }
            catch (Exception exception)
            {
                if (Session.IsInTransaction)
                {
                    await Session.AbortTransactionAsync();
                    Committed = true;
                }

                if (exception is MongoException mongoException)
                {
                    _logger.LogError($"Update Error, ObjType:[${obj.GetType()}], ObjId:[{obj.Id}]" +
                                     $", Message;[{mongoException.Message}]" +
                                     $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");
                }

                throw;
            }

        }

        /// <summary> 
        /// 批量删除聚合根
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型。</typeparam>
        /// <param name="objs">需要删除的聚合根列表。</param>
        public override async Task RegisterDeleted<TAggregateRoot>(IList<TAggregateRoot> objs)
        {
            try
            {
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
            }
            catch (Exception exception)
            {
                if (Session.IsInTransaction)
                {
                    await Session.AbortTransactionAsync();
                    Committed = true;
                }

                if (exception is MongoException mongoException)
                {
                    _logger.LogError($"Bulk Delete Error, ObjType:[${objs.First().GetType()}]" +
                                     $", ObjCount: [{objs.Count}]",
                        $", Message;[{mongoException.Message}]" +
                        $", ErrorLabels :[{string.Join(",", mongoException.ErrorLabels ?? new string[0])}]");

                }

                throw;
            }
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
                            if (exception.HasErrorLabel("UnknownTransactionCommitResult"))
                            {
                                if (count < 3)
                                {
                                    continue;
                                }

                                throw;
                            }

                            throw;
                        }
                    }
                }

                Committed = true;
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
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Session != null)
            {
                this.Session.Dispose();
            }
        }

        #region Public Static Methods
        /// <summary>
        /// Registers the MongoDB Bson serialization conventions.
        /// </summary>
        /// <param name="additionConventions">Additional conventions that needs to be registered.</param>
        public static void RegisterConventions(IEnumerable<IConvention> additionConventions = null)
        {
            var conventionPack = new ConventionPack();
            conventionPack.Add(new NamedIdMemberConvention("id", "_id"));
            conventionPack.Add(new IgnoreExtraElementsConvention(true));
            conventionPack.Add(new StringObjectIdIdGeneratorConvention());
            //枚举序列化为字符串
            conventionPack.Add(new EnumRepresentationConvention(BsonType.String));

            if (additionConventions != null)
                conventionPack.AddRange(additionConventions);

            ConventionRegistry.Register("DefaultConvention", conventionPack, t => true);

            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

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
