//using MediatR;
//using MySvc.Framework.Domain.Core.DomainEvents;

//namespace MySvc.Framework.Domain.Core.Extensions
//{
//    public class DomainEventDispatcher : IDomainEventDispatcher
//    {
//        private readonly IMediator _mediator;
//        public DomainEventDispatcher(IMediator mediator) {
//            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
//        }
//        public async Task Dispatch(IDomainEvent domainEvent)
//        {
//            if (domainEvent == null)
//            {
//                throw new ArgumentNullException(nameof(domainEvent));
//            }

//            var domainEventNotification = CreateDomainEventNotification(domainEvent);
//            await _mediator.Publish(domainEvent);
//        }

//        private INotification CreateDomainEventNotification(IDomainEvent domainEvent)
//        {
//            var genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
//            return (INotification)Activator.CreateInstance(genericDispatcherType, domainEvent);

//        }
//    }
