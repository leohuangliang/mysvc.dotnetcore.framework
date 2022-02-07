using System.Collections.Generic;
using MySvc.Framework.Domain.Core.DomainEvents;
using MySvc.Framework.Domain.Core.Models;
using System;

namespace MySvc.Framework.Domain.Core.Impl
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {

        /// <summary>
        /// 
        /// </summary>
        protected AggregateRoot() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected AggregateRoot(string id) : base(id)
        {

        }

        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents
        {
            get
            {
                if (_domainEvents == null) _domainEvents = new List<IDomainEvent>();
                return _domainEvents.AsReadOnly();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainEvent"></param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainEvent"></param>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; protected set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Operator Creator { get; protected set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime ModifiedOn { get; protected set; }


        /// <summary>
        /// 最后更新人
        /// </summary>
        public Operator ModifiedBy { get; protected set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(AggregateRoot left, AggregateRoot right)
        {
            if (Equals(left, null))
                return (Equals(right, null)) ? true : false;
            return left.Equals(right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(AggregateRoot left, AggregateRoot right)
        {
            return !(left == right);
        }

        #endregion
    }
}
