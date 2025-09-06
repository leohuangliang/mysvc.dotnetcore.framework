using MassTransit;
using Microsoft.Extensions.Logging;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Domain.Core.Impl;
using MySvc.Framework.Infrastructure.Crosscutting.EventBus;
using MySvc.Framework.Infrastructure.Crosscutting.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.IntegrationEventService
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
            _integrationEventLogRepository = integrationEventLogRepository ?? throw new ArgumentNullException(nameof(integrationEventLogRepository));
            _integrationEventLogManager = integrationEventLogManager ?? throw new ArgumentNullException(nameof(integrationEventLogManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jsonConverter = jsonConverter ?? throw new ArgumentNullException(nameof(jsonConverter));
        }

        /// <summary>
        /// 保存事件，确保在本地事物内完成
        /// </summary>
        /// <param name="event">集成事件</param>
        public async Task SaveIntegrationEvent<T>(T @event) where T : class
        {

            var integrationEventLog = new IntegrationEventLog(Guid.NewGuid(), DateTime.UtcNow, @event.GetType().FullName ?? string.Empty,
                _jsonConverter.SerializeObject(@event));
            await _integrationEventLogRepository.AddAsync(integrationEventLog);
            //事件入内存队列

            _messageQueue.Enqueue(new KeyValuePair<Guid, object>(integrationEventLog.EventId, @event));

        }

        /// <summary>
        /// 保存事件，确保在本地事物内完成
        /// </summary>
        /// <param name="event">集成事件</param>
        public async Task SaveIntegrationEvent<T>(object @event) where T : class
        {

            var integrationEventLog = new IntegrationEventLog(Guid.NewGuid(), DateTime.UtcNow, typeof(T).FullName ?? string.Empty,
                _jsonConverter.SerializeObject(@event));
            await _integrationEventLogRepository.AddAsync(integrationEventLog);
            //事件入内存队列

            _messageQueue.Enqueue(new KeyValuePair<Guid, dynamic>(integrationEventLog.EventId, @event));

        }

        /// <summary>
        /// 批量保存集成事件
        /// </summary>
        /// <param name="evts">集成事件对象列表</param>
        public async Task SaveIntegrationEvent<T>(IList<T> evts) where T : class
        {
            if (evts != null && evts.Any())
            {
                foreach (var integrationEvent in evts)
                {
                    var integrationEventLog = new IntegrationEventLog(Guid.NewGuid(), DateTime.UtcNow, integrationEvent.GetType().FullName ?? string.Empty,
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
        public async Task SaveIntegrationEvent<T>(IList<object> evts) where T : class
        {
            if (evts != null && evts.Any())
            {
                foreach (var integrationEvent in evts)
                {
                    var integrationEventLog = new IntegrationEventLog(Guid.NewGuid(), DateTime.UtcNow, typeof(T).FullName ?? string.Empty,
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

        public Task PublishIntegrationEventWithoutSave<T>(T @event) where T : class
        {
            if (@event != null)
            {
                _publishEndpoint.Publish<T>(@event);
            }

            return Task.CompletedTask;

        }
        public Task PublishIntegrationEventWithoutSave<T>(object @event) where T : class
        {
            if (@event != null)
            {
                _publishEndpoint.Publish(@event);
            }

            return Task.CompletedTask;

        }
    }
}
