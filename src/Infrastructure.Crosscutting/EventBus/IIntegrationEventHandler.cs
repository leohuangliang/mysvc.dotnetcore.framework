using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events;
using DotNetCore.CAP;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus
{
    /// <summary>
    /// 表示集成事件的处理器
    /// </summary>
    public interface IIntegrationEventHandler : ICapSubscribe
    {
        
    }

    /// <summary>
    /// 表示集成事件的处理器，基于特定的事件
    /// </summary>
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IIntegrationEvent
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event">集成事件对象</param>
        
        Task Handle(TIntegrationEvent @event);
    }
}