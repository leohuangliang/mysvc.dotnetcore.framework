using System;
using MySvc.Framework.Domain.Core.Attributes;

namespace MySvc.Framework.Domain.Core.Impl
{
    /// <summary>
    /// 集成事件Log聚合根
    /// </summary>
    [AggregateRootName("IntegrationEventLogs")]
    public class IntegrationEventLog : AggregateRoot
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <param name="eventCreatedTime">事件创建时间</param>
        /// <param name="eventTypeFullName">事件完整类型</param>
        /// <param name="eventContent">事件内容</param>
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

        /// <summary>
        /// 设置已发送
        /// </summary>
        public void SetPublished()
        {
            State = EventStateEnum.Published;
            EventPublishedTime = DateTime.UtcNow;
        }

        /// <summary>
        /// 设置发布失败
        /// </summary>
        public void SetPublishFailed()
        {
            State = EventStateEnum.PublishFailed;
            EventPublishedFailedTime = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// 事件状态枚举
    /// </summary>
    public enum EventStateEnum
    {
        /// <summary>
        /// 未发送
        /// </summary>
        NotPublished,
        /// <summary>
        /// 已发送
        /// </summary>
        Published,
        /// <summary>
        /// 发送失败
        /// </summary>
        PublishFailed
    }
}
