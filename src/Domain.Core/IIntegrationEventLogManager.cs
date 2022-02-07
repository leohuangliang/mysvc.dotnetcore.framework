using System;
using System.Threading.Tasks;

namespace MySvc.Framework.Domain.Core
{
    /// <summary>
    /// 集成事件Log管理器
    /// </summary>
    public interface IIntegrationEventLogManager
    {
        /// <summary>
        /// 标记事件已经发送成功
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <returns></returns>
        Task MarkEventLogAsPublishedAsync(Guid eventId);
    }
}
