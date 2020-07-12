using Contracts.Events;
using MediatR;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Order.Application.Commands.Handlers
{
    /// <summary>
    /// 注册租户账号
    /// </summary>
    public class TenantRegisterActivatedCommandHandler : IRequestHandler<TenantRegisterActivatedCommand, bool>
    {
        private readonly IDBContext _dbContext;


        private readonly ITypeAdapter _typeAdapter;
        private readonly IIntegrationEventService _integrationEventService;

        public TenantRegisterActivatedCommandHandler(IDBContext dbContext, ITypeAdapter typeAdapter, IIntegrationEventService integrationEventService)
        {
            _dbContext = dbContext;

            _typeAdapter = typeAdapter;
            _integrationEventService = integrationEventService ?? throw new System.ArgumentNullException(nameof(integrationEventService));

        }

        public async Task<bool> Handle(TenantRegisterActivatedCommand command, CancellationToken cancellationToken)
        {
            _dbContext.BeginTransaction();
            //保存集成事件
            await _integrationEventService.SaveIntegrationEvent(
                new TenantRegisterActivatedIntegrationEvent(command.TenantCode, command.TenantOwnerUserName, command.CreateTime, command.ActivationTime));

            await _dbContext.CommitAsync();
            await _integrationEventService.PublishAllAsync();
            return true;
        }
    }
}
