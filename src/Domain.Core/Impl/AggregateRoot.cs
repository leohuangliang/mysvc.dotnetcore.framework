using System.Collections.Generic;
using MySvc.Framework.Domain.Core.DomainEvents;
using MySvc.Framework.Domain.Core.Models;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;
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
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
            _keywords = new HashSet<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected AggregateRoot(string id) : base(id)
        {

        }

        private List<IDomainEvent>? _domainEvents;

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
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; } 
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainEvent"></param>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        

        /// <summary>
        /// 创建人
        /// </summary>
        public Operator Creator { get; protected set; } = Operator.CreateSystemOperator();

        /// <summary>
        /// 最后更新人
        /// </summary>
        public Operator ModifiedBy { get; protected set; } = Operator.CreateSystemOperator();


        protected HashSet<string> _keywords = new HashSet<string>();
        /// <summary>
        /// 关键字 用来查询
        /// </summary>
        public IReadOnlyCollection<string> Keywords
        {
            get => _keywords ??= new HashSet<string>();
            private set => _keywords = new HashSet<string>(value);
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
        public override bool Equals(object? obj)
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
        /// 相等操作符，支持可空类型
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        /// <returns>如果两个聚合根相等则返回true，否则返回false</returns>
        public static bool operator ==(AggregateRoot? left, AggregateRoot? right)
        {
            // 如果两个都是null，返回true
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            
            // 如果其中一个是null，返回false
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            
            // 都不是null时，调用Equals方法
            return left.Equals(right);
        }

        /// <summary>
        /// 不等操作符，支持可空类型
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        /// <returns>如果两个聚合根不相等则返回true，否则返回false</returns>
        public static bool operator !=(AggregateRoot? left, AggregateRoot? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// 更新关键字
        /// </summary>
        /// <param name="keywords"></param>
        public void UpdateOrInsertKeywords(params string[] keywords)
        {
            foreach (var key in keywords)
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    string lowerKeyword = key.ToLower();
                    if (!_keywords.Contains(lowerKeyword))
                    {
                        _keywords.Add(lowerKeyword);
                    }
                }
            }
        }

        /// <summary>
        /// 清理关键字
        /// </summary>
        public void ClearKeywords()
        {
            _keywords.Clear();
        }

        #endregion


    }
}

