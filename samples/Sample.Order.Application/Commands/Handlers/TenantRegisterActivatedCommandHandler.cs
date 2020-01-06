using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using MediatR;
using Sample.Order.Application.Extensions;
using Sample.Order.Application.IntegrationEvents.Events;
using Sample.Order.Domain.AggregatesModel.OrderAggregate;
using Sample.Order.Domain.Repositories;

namespace Sample.Order.Application.Commands.Handlers
{
    /// <summary>
    /// 注册租户账号
    /// </summary>
    public class TenantRegisterActivatedCommandHandler : IRequestHandler<TenantRegisterActivatedCommand, bool>
    {
        private readonly IDBContext _dbContext;


        private readonly ITypeAdapter _typeAdapter;
        private readonly IEventBus _eventBus;

        public TenantRegisterActivatedCommandHandler(IDBContext dbContext, ITypeAdapter typeAdapter, IEventBus eventBus)
        {
            _dbContext = dbContext;
            
            _typeAdapter = typeAdapter;
            _eventBus = eventBus ?? throw new System.ArgumentNullException(nameof(eventBus));
            
        }

        public async Task<bool> Handle(TenantRegisterActivatedCommand command, CancellationToken cancellationToken)
        {
            _dbContext.BeginTransaction();
            //保存集成事件
            await _eventBus.Publish(
                new TenantRegisterActivatedIntegrationEvent(command.TenantCode,command.TenantOwnerUserName,command.CreateTime,command.ActivationTime));

            await _dbContext.CommitAsync();
            return true;
        }
    }
}
