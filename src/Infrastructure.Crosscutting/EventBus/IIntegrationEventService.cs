using System.Collections.Generic;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus.Events;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus
{
    /// <summary>
    /// 集成事件服务
    /// </summary>
    public interface IIntegrationEventService
    {
        /// <summary>
        /// 保存集成事件
        /// </summary>
        /// <param name="evt">集成事件对象</param>
        Task SaveIntegrationEvent(IIntegrationEvent evt);

        /// <summary>
        /// 批量保存集成事件
        /// </summary>
        /// <param name="evts">集成事件对象列表</param>
        Task SaveIntegrationEvent(IList<IIntegrationEvent> evts);
        /// <summary>
        /// 发布所有消息
        /// </summary>
        Task PublishAllAsync();

    }
}
