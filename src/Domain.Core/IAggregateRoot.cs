using System.Collections.Generic;
using MySvc.DotNetCore.Framework.Domain.Core.DomainEvents;

namespace MySvc.DotNetCore.Framework.Domain.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainEvent"></param>
        void AddDomainEvent(IDomainEvent domainEvent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainEvent"></param>
        void RemoveDomainEvent(IDomainEvent domainEvent);

        /// <summary>
        /// 
        /// </summary>
        void ClearDomainEvents();
    }
}