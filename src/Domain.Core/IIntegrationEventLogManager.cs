using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events;

namespace MySvc.DotNetCore.Framework.Domain.Core
{
    public interface IIntegrationEventLogManager
    {
        /// <summary>
        /// 标记事件已经发送成功
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task MarkEventLogAsPublishedAsync(IIntegrationEvent @event);
    }
}
