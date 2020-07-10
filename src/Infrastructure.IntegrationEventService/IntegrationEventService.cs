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
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events;
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

        private readonly Queue<IIntegrationEvent> _messageQueue = new Queue<IIntegrationEvent>();

        public IntegrationEventService(IPublishEndpoint publishEndpoint,
            IIntegrationEventLogRepository integrationEventLogRepository,
            IIntegrationEventLogManager integrationEventLogManager,
            ILogger<IntegrationEventService> logger,
            IJsonConverter jsonConverter)
        {
            _messageQueue = new Queue<IIntegrationEvent>();
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
        public async Task SaveIntegrationEvent(IIntegrationEvent @event)
        {
            var exist = await _integrationEventLogRepository.ExistsAsync(
                    Specification<IntegrationEventLog>.Eval(c => c.EventId == @event.Id));

            if (!exist)
            {
                var integrationEventLog = new IntegrationEventLog(@event.Id, @event.CreatedTime, @event.GetType().FullName,
                    _jsonConverter.SerializeObject(@event));
                await _integrationEventLogRepository.AddAsync(integrationEventLog);
                //事件入内存队列
                _messageQueue.Enqueue(@event);
            }
        }

        /// <summary>
        /// 批量保存集成事件
        /// </summary>
        /// <param name="evts">集成事件对象列表</param>
        public async Task SaveIntegrationEvent(IList<IIntegrationEvent> evts)
        {
            if (evts != null && evts.Any())
            {
                var eventIds = evts.Select(x => x.Id).Distinct().ToList();
                //查询已经保存的集成事件
                var exists = (await _integrationEventLogRepository.GetListAsync(
                    Specification<IntegrationEventLog>.Eval(c => eventIds.Contains(c.EventId)))).ToList();

                if (exists.Any())
                {
                    var existIds = exists.Select(x => x.Id).ToList();

                    //保留未保存的集成事件
                    evts = evts.Where(x => !existIds.Contains(x.Id.ToString())).ToList();
                }

                var integrationEventLogs = evts.Select(x => new IntegrationEventLog(x.Id, x.CreatedTime,
                    x.GetType().FullName, _jsonConverter.SerializeObject(x))).ToList();

                if (integrationEventLogs.Any())
                {
                    await _integrationEventLogRepository.AddAsync(integrationEventLogs);

                    //事件入内存队列
                    foreach (var evt in evts)
                    {
                        _messageQueue.Enqueue(evt);
                    }
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
                    var @event = _messageQueue.Dequeue();
                    _publishEndpoint.Publish(@event); //脱离DBContext上下文，不依赖事务
                    _integrationEventLogManager.MarkEventLogAsPublishedAsync(@event);
                }
            });

            return task;
        }

        /// <summary>
        /// 直接发布集成事件（此操作，不保存集成事件）
        /// </summary>
        /// <param name="events">集成事件</param>
        public Task PublishIntegrationEvents(IList<IIntegrationEvent> events)
        {
            if (events != null && events.Count > 0)
            {
                var localEvents = events.ToList();
                Task.Run(() =>
                {
                    foreach (var @event in localEvents)
                    {
                        _publishEndpoint.Publish(@event);
                    }
                });
            }

            return Task.CompletedTask;
        }
    }
}
