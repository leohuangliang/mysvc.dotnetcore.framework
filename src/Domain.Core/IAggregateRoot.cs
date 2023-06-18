using System;
using System.Collections.Generic;
using MySvc.Framework.Domain.Core.DomainEvents;

namespace MySvc.Framework.Domain.Core
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

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime ModifiedOn { get; set; }

    }
}