using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;
using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;

namespace MySvc.DotNetCore.Framework.Infrastructure.IntegrationEventService
{
    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IIntegrationEventLogRepository _integrationEventLogRepository;
        private readonly IIntegrationEventLogManager _integrationEventLogManager;
        private readonly ILogger<IntegrationEventService> _logger;
        private readonly IJsonConverter _jsonConverter;

        private readonly Queue<KeyValuePair<Guid, object>> _messageQueue;

        public IntegrationEventService(IPublishEndpoint publishEndpoint,
            IIntegrationEventLogRepository integrationEventLogRepository,
            IIntegrationEventLogManager integrationEventLogManager,
            ILogger<IntegrationEventService> logger,
            IJsonConverter jsonConverter)
        {
            _messageQueue = new Queue<KeyValuePair<Guid, dynamic>>();
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            if (integrationEventLogRepository != null) _integrationEventLogRepository = integrationEventLogRepository;
            if (integrationEventLogManager != null) _integrationEventLogManager = integrationEventLogManager;
            if (logger != null) _logger = logger;
            if (jsonConverter != null) _jsonConverter = jsonConverter;
        }

        /// <summary>
        /// 保存事件，确保在本地事物内完成
        /// </summary>
        /// <param name="event">集成事件</param>
        public async Task SaveIntegrationEvent<T>(T @event) where T : class, new()
        {

            var integrationEventLog = new IntegrationEventLog(Guid.NewGuid(), DateTime.UtcNow, @event.GetType().FullName,
                _jsonConverter.SerializeObject(@event));
            await _integrationEventLogRepository.AddAsync(integrationEventLog);
            //事件入内存队列

            _messageQueue.Enqueue(new KeyValuePair<Guid, object>(integrationEventLog.EventId, @event));

        }

        /// <summary>
        /// 保存事件，确保在本地事物内完成
        /// </summary>
        /// <param name="event">集成事件</param>
        public async Task SaveIntegrationEvent<T>(object @event) where T : class, new()
        {

            var integrationEventLog = new IntegrationEventLog(Guid.NewGuid(), DateTime.UtcNow, typeof(T).FullName,
                _jsonConverter.SerializeObject(@event));
            await _integrationEventLogRepository.AddAsync(integrationEventLog);
            //事件入内存队列

            _messageQueue.Enqueue(new KeyValuePair<Guid, dynamic>(integrationEventLog.EventId, @event));

        }

        /// <summary>
        /// 批量保存集成事件
        /// </summary>
        /// <param name="evts">集成事件对象列表</param>
        public async Task SaveIntegrationEvent<T>(IList<T> evts) where T : class, new()
        {
            if (evts != null && evts.Any())
            {
                foreach (var integrationEvent in evts)
                {
                    var integrationEventLog = new IntegrationEventLog(Guid.NewGuid(), DateTime.UtcNow, integrationEvent.GetType().FullName,
                        _jsonConverter.SerializeObject(integrationEvent));
                    await _integrationEventLogRepository.AddAsync(integrationEventLog);
                    //事件入内存队列

                    _messageQueue.Enqueue(new KeyValuePair<Guid, dynamic>(integrationEventLog.EventId, integrationEvent));
                }
            }
        }

        /// <summary>
        /// 批量保存集成事件
        /// </summary>
        /// <param name="evts">集成事件对象列表</param>
        public async Task SaveIntegrationEvent<T>(IList<object> evts) where T : class, new()
        {
            if (evts != null && evts.Any())
            {
                foreach (var integrationEvent in evts)
                {
                    var integrationEventLog = new IntegrationEventLog(Guid.NewGuid(), DateTime.UtcNow, typeof(T).FullName,
                        _jsonConverter.SerializeObject(integrationEvent));
                    await _integrationEventLogRepository.AddAsync(integrationEventLog);
                    //事件入内存队列

                    _messageQueue.Enqueue(new KeyValuePair<Guid, dynamic>(integrationEventLog.EventId, integrationEvent));
                }
            }
        }

        /// <summary>
        /// 发布全部事件，不依赖本地事务
        /// </summary>
        public Task PublishAllAsync()
        {
            var task = Task.Run(() =>
            {
                while (_messageQueue.Count > 0)
                {
                    var kvp = _messageQueue.Dequeue();

                    _publishEndpoint.Publish(kvp.Value); 
                    _integrationEventLogManager.MarkEventLogAsPublishedAsync(kvp.Key);
                }
            });

            return task;
        }

        public Task PublishIntegrationEventWithoutSave<T>(T @event) where T : class, new()
        {
            if (@event != null)
            {
                _publishEndpoint.Publish<T>(@event);
            }

            return Task.CompletedTask;

        }
        public Task PublishIntegrationEventWithoutSave<T>(object @event) where T : class, new()
        {
            if (@event != null)
            {
                _publishEndpoint.Publish(@event);
            }

            return Task.CompletedTask;

        }
    }
}
