using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task SaveIntegrationEvent<T>(T evt) where T : class;

        /// <summary>
        /// 批量保存集成事件
        /// </summary>
        /// <param name="evts">集成事件对象列表</param>
        Task SaveIntegrationEvent<T>(IList<T> evts) where T : class;
        /// <summary>
        /// 发布所有消息
        /// </summary>
        Task PublishAllAsync();

        Task PublishIntegrationEventWithoutSave<T>(T @event) where T : class;

    }
}
