using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BFE.Framework.Infrastructure.Crosscutting.EventBus.Events;

namespace BFE.Framework.Domain.Core.DomainServices
{
    public interface IIntegrationEventLogService
    {
        /// <summary>
        /// 保存集成事件
        /// </summary>
        /// <param name="event">领域事件</param>
        /// <returns></returns>
        Task SaveEventLogAsync(IIntegrationEvent @event);
    }
}
