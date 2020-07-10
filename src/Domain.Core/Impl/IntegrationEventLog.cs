using System;
using MySvc.DotNetCore.Framework.Domain.Core.Attributes;

namespace MySvc.DotNetCore.Framework.Domain.Core.Impl
{
    /// <summary>
    /// 集成事件Log聚合根
    /// </summary>
    [AggregateRootName("IntegrationEventLogs")]
    public class IntegrationEventLog : AggregateRoot
    {
        public IntegrationEventLog(
            Guid eventId,
            DateTime eventCreatedTime,
            string eventTypeFullName,
            string eventContent
            )

        {
            EventId = eventId;
            EventCreatedTime = eventCreatedTime;
            EventTypeFullName = eventTypeFullName;
            Content = eventContent;
            State = EventStateEnum.NotPublished;
            TimesSent = 0;
            LogCreatedTime = DateTime.UtcNow;
        }

        /// <summary>
        /// 集成事件Id
        /// </summary>
        public Guid EventId { get; private set; }

        /// <summary>
        /// 集成事件
        /// </summary>
        public string EventTypeFullName { get; private set; }

        /// <summary>
        /// 事件发送状态
        /// </summary>
        public EventStateEnum State { get; private set; }

        /// <summary>
        /// 事件发送次数
        /// </summary>
        public int TimesSent { get; set; }

        /// <summary>
        /// 事件创建时间
        /// </summary>
        public DateTime EventCreatedTime { get; private set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 事件Log创建时间
        /// </summary>
        public DateTime LogCreatedTime { get; private set; }

        /// <summary>
        /// 事件发送成功时间
        /// </summary>
        public DateTime? EventPublishedTime { get; private set; }

        /// <summary>
        /// 事件发送失败时间
        /// </summary>
        public DateTime? EventPublishedFailedTime { get; private set; }

        public void SetPublished()
        {
            State = EventStateEnum.Published;
            EventPublishedTime = DateTime.UtcNow;
        }

        public void SetPublishedFailed()
        {
            State = EventStateEnum.PublishedFailed;
            EventPublishedFailedTime = DateTime.UtcNow;
        }
    }

    public enum EventStateEnum
    {
        NotPublished,
        Published,
        PublishedFailed
    }
}
