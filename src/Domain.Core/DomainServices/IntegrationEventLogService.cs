using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BFE.Framework.Domain.Core.Impl;
using BFE.Framework.Domain.Core.Specification;
using BFE.Framework.Infrastructure.Crosscutting.EventBus.Events;
using BFE.Framework.Infrastructure.Crosscutting.Json;

namespace BFE.Framework.Domain.Core.DomainServices
{
    public class IntegrationEventLogService : IIntegrationEventLogService
    {
        private readonly IIntegrationEventLogRepository _integrationEventLogRepository;
        private readonly IJsonConverter _jsonConverter;
        public IntegrationEventLogService(IIntegrationEventLogRepository integrationEventLogRepository, IJsonConverter jsonConverter)
        {
            _integrationEventLogRepository = integrationEventLogRepository;
            _jsonConverter = jsonConverter;
        }

        /// <summary>
        /// 保存事件日志
        /// </summary>
        /// <param name="event">集成事件</param>
        /// <returns></returns>
        public async Task SaveEventLogAsync(IIntegrationEvent @event)
        {
            bool exist =
                await _integrationEventLogRepository.ExistsAsync(
                    Specification<IntegrationEventLog>.Eval(c => c.EventId == @event.Id));

            if (!exist)
            {
                var integrationEventLog = new IntegrationEventLog(@event.Id, @event.CreatedTime, @event.GetType().FullName,
                    _jsonConverter.SerializeObject(@event));
                await _integrationEventLogRepository.AddAsync(integrationEventLog);
            }
        }
    }
}
