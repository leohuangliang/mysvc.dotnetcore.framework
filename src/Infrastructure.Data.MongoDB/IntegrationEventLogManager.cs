﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Domain.Core.Attributes;
using MySvc.Framework.Domain.Core.Impl;
using MySvc.Framework.Infrastructure.Crosscutting.Exceptions;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.Framework.Infrastructure.Crosscutting.Options;

namespace MySvc.Framework.Infrastructure.Data.MongoDB
{
    /// <summary>
    /// 维持一个独立的不跨事物的事件日志管理器
    /// </summary>
    public class IntegrationEventLogManager : IIntegrationEventLogManager
    {

        public IntegrationEventLogManager(IOptions<MongoDBSettings> mongoDBSettingsAccessor)
        {
            var mongoDbSettings = mongoDBSettingsAccessor.Value;
            Client = new MongoClient(mongoDbSettings.ConnectionString);
            Database = Client.GetDatabase(mongoDbSettings.Database);
        }

        IMongoClient Client { get; }
        IMongoDatabase Database { get; }

        /// <summary>
        /// 标记事件日志为已发送状态
        /// </summary>
        /// <param name="eventId">事件Id</param>
        /// <returns></returns>
        public async Task MarkEventLogAsPublishedAsync(Guid eventId)
        {
            var collection = Database.GetCollection<IntegrationEventLog>(GetAttributeCollectionName(typeof(IntegrationEventLog)) ?? this.Pluralize(typeof(IntegrationEventLog)));

            var eventLogEntry =
                await collection.Find(Builders<IntegrationEventLog>.Filter.Eq(c => c.EventId, eventId)).FirstOrDefaultAsync();
            if (eventLogEntry != null)
            {
                eventLogEntry.TimesSent++;
                eventLogEntry.SetPublished();

                string originVersion = eventLogEntry.Timestamp;

                var builder = Builders<IntegrationEventLog>.Filter;
                var filter = builder.And(builder.Eq(c => c.Id, eventLogEntry.Id), builder.Eq(c => c.Timestamp, originVersion));
                eventLogEntry.Timestamp = DateTimeOffset.UtcNow.Ticks.ToString();

                var res = await collection.ReplaceOneAsync(filter, eventLogEntry,
                    new ReplaceOptions() { IsUpsert = false });

                if (res == null)
                {
                    throw new ConcurrencyException($"{eventLogEntry.GetType().ToString()} Id:  {eventLogEntry.Id}, 更新时发生并发性错误, OriginVersion: {originVersion}");
                }
            }
        }

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
