using System.Collections.Generic;
using MySvc.DotNetCore.Framework.Domain.Core.DomainEvents;

namespace MySvc.DotNetCore.Framework.Domain.Core
{
    public interface IAggregateRoot : IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void AddDomainEvent(IDomainEvent domainEvent);
        void RemoveDomainEvent(IDomainEvent domainEvent);
        void ClearDomainEvents();
    }
}