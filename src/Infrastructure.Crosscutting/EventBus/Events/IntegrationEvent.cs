using System;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events
{
    /// <summary>
    /// 集成事件基础类
    /// </summary>
    public abstract class IntegrationEvent : IIntegrationEvent
    {
        protected IntegrationEvent()
        {
            this.Id = Guid.NewGuid();
            this.CreatedTime = DateTime.UtcNow;
        }

        /// <summary>
        /// 事件唯一标识
        /// </summary>
        public  Guid Id { get; }

        /// <summary>
        /// 事件创建的时间
        /// </summary>
        public DateTime CreatedTime { get; }
    }
}
