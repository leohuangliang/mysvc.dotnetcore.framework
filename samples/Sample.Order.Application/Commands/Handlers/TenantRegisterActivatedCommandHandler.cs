using Contracts.Events;
using MediatR;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Infrastructure.Crosscutting.EventBus;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Sample.Order.Application.Commands.Handlers
{
    /// <summary>
    /// 注册租户账号
    /// </summary>
    public class TenantRegisterActivatedCommandHandler : IRequestHandler<TenantRegisterActivatedCommand, bool>
    {
        private readonly IDBContext _dbContext;
        private readonly IIntegrationEventService _integrationEventService;

        public TenantRegisterActivatedCommandHandler(IDBContext dbContext,IIntegrationEventService integrationEventService)
        {
            _dbContext = dbContext;

            _integrationEventService = integrationEventService ?? throw new System.ArgumentNullException(nameof(integrationEventService));

        }

        public async Task<bool> Handle(TenantRegisterActivatedCommand command, CancellationToken cancellationToken)
        {
            _dbContext.BeginTransaction();
            //保存集成事件
            await _integrationEventService.SaveIntegrationEvent<TenantRegisterActivatedIntegrationEvent>(
                new TenantRegisterActivatedIntegrationEvent
                {
                    TenantCode = command.TenantCode,
                    TenantOwnerUserName = command.TenantOwnerUserName,
                    CreateTime = command.CreateTime,
                    ActivationTime = command.ActivationTime
                });

            await _dbContext.CommitAsync();
            await _integrationEventService.PublishAllAsync();
            return true;
        }
    }
}
