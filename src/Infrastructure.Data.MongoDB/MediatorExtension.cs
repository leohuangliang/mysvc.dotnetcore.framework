using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySvc.Framework.Domain.Core;
using MediatR;

namespace MySvc.Framework.Infrastructure.Data.MongoDB
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, IAggregateRoot aggregateRoot)
        {

            var domainEvents = aggregateRoot.DomainEvents.ToList();

            aggregateRoot.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)//将领域事件执行调整成顺序执行
            {
                await mediator.Publish(domainEvent);
            }

            //var tasks = domainEvents
            //    .Select(async (domainEvent) => {
            //        await mediator.Publish(domainEvent);
            //    });

            //await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 分发领域事件
        /// </summary>
        /// <param name="mediator">IMediator示例实例</param>
        /// <param name="aggregateRoots">聚合根对象</param>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, IList<IAggregateRoot> aggregateRoots)
        {
            foreach (var aggregateRoot in aggregateRoots)
            {
                await mediator.DispatchDomainEventsAsync(aggregateRoot);
            }
        }
    }
}
