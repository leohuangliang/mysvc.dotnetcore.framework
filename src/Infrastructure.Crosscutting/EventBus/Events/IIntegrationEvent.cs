using System;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events
{
    /// <summary>
    /// 表示一个集成事件
    /// </summary>
    public interface IIntegrationEvent
    {
        /// <summary>
        /// 事件唯一标识
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 事件创建的时间
        /// </summary>
        DateTime CreatedTime { get; }
    }
}
