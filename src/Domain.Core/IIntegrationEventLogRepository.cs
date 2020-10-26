using MySvc.DotNetCore.Framework.Domain.Core.Impl;

namespace MySvc.DotNetCore.Framework.Domain.Core
{
    /// <summary>
    /// 集成事件Log仓储接口
    /// </summary>
    public interface IIntegrationEventLogRepository : IRepository<IntegrationEventLog>
    {
    }
}
