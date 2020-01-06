using System.Collections.Generic;
using MySvc.DotNetCore.Framework.Domain.Core.DomainEvents;

namespace MySvc.DotNetCore.Framework.Domain.Core.Impl
{

    public abstract class AggregateRoot : Entity, IAggregateRoot
    {

        protected AggregateRoot() : base()
        {

        }

        protected AggregateRoot(string id) : base(id)
        {

        }

        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents
        {
            get
            {
                if (_domainEvents == null) _domainEvents = new List<IDomainEvent>();
                return _domainEvents.AsReadOnly();
            }
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        #region Override Methods

        /// <summary>
        ///     确定指定的Object是否等于当前的Object。
        /// </summary>
        /// <param name="obj">要与当前对象进行比较的对象。</param>
        /// <returns>如果指定的Object与当前Object相等，则返回true，否则返回false。</returns>
        /// <remarks>
        ///     有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.equals。
        /// </remarks>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            var ar = obj as IAggregateRoot;
            if (ar == null)
                return false;
            return base.Id == ar.Id;
        }

        /// <summary>
        ///     用作特定类型的哈希函数。
        /// </summary>
        /// <returns>当前Object的哈希代码。</returns>
        /// <remarks>
        ///     有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.gethashcode。
        /// </remarks>
        public override int GetHashCode()
        {
            return base.Id.GetHashCode();
        }

        public static bool operator ==(AggregateRoot left, AggregateRoot right)
        {
            if (Equals(left, null))
                return (Equals(right, null)) ? true : false;
            return left.Equals(right);
        }

        public static bool operator !=(AggregateRoot left, AggregateRoot right)
        {
            return !(left == right);
        }

        #endregion
    }
}
