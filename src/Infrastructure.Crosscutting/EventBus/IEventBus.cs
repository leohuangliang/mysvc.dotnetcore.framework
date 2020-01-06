using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events;
using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus
{
    public interface IEventBus
    {
        /// <summary>
        /// 发布集成事件
        /// </summary>
        /// <param name="event">集成事件</param>
        Task Publish(IIntegrationEvent @event);
    }
}
