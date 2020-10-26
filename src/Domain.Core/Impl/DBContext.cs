using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting;

namespace MySvc.DotNetCore.Framework.Domain.Core.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DBContext : DisposableObject, IDBContext
    {
        #region Private Fields
        private readonly Guid _id = Guid.NewGuid();
        /// <summary>
        /// 
        /// </summary>
        protected readonly AsyncLocal<Dictionary<string, object>> _localNewCollection =
            new AsyncLocal<Dictionary<string, object>> { Value = new Dictionary<string, object>() };
        /// <summary>
        /// 
        /// </summary>
        protected readonly AsyncLocal<Dictionary<string, object>> _localModifiedCollection =
            new AsyncLocal<Dictionary<string, object>> { Value = new Dictionary<string, object>() };
        /// <summary>
        /// 
        /// </summary>
        protected readonly AsyncLocal<Dictionary<string, object>> _localDeletedCollection =
            new AsyncLocal<Dictionary<string, object>> { Value = new Dictionary<string, object>() };
        /// <summary>
        /// 
        /// </summary>
        protected readonly AsyncLocal<bool> _localCommitted =
            new AsyncLocal<bool> { Value = true };
        private readonly object _sync = new object();
        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        protected DBContext()
        {
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Clears all the registration in the repository context.
        /// </summary>
        /// <remarks>Note that this can only be called after the repository context has successfully committed.</remarks>
        protected void ClearRegistrations()
        {
            this._localNewCollection.Value.Clear();
            this._localModifiedCollection.Value.Clear();
            this._localDeletedCollection.Value.Clear();
        }

        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be added to the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<string, object>> NewCollection
        {
            get { return _localNewCollection.Value; }
        }
        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be modified in the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<string, object>> ModifiedCollection
        {
            get { return _localModifiedCollection.Value; }
        }
        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be deleted from the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<string, object>> DeletedCollection
        {
            get { return _localDeletedCollection.Value; }
        }
        #endregion

        #region IRepositoryContext Members
        /// <summary>
        /// Gets the Id of the repository context.
        /// </summary>
        public Guid Id
        {
            get { return _id; }
        }
        /// <summary>
        /// Registers a new object to the repository context.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual async Task RegisterNew<TAggregateRoot>(TAggregateRoot obj) where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.IsTransient())
            {
                obj.GenerateId();
            }
            if (_localModifiedCollection.Value.ContainsKey(obj.Id))
                throw new InvalidOperationException("The object cannot be registered as a new object since it was marked as modified.");
            if (_localNewCollection.Value.ContainsKey(obj.Id))
                throw new InvalidOperationException("The object has already been registered as a new object.");
            _localNewCollection.Value.Add(obj.Id, obj);
            _localCommitted.Value = false;

            await Task.CompletedTask;
        }

        /// <summary> 
        /// ÅúÁ¿½«Ö¸¶¨µÄ¾ÛºÏ¸ù±ê×¢Îª¡°ÐÂ½¨¡±×´Ì¬¡£
        /// </summary>
        /// <typeparam name="TAggregateRoot">ÐèÒª±ê×¢×´Ì¬µÄ¾ÛºÏ¸ùÀàÐÍ¡£</typeparam>
        /// <param name="objs">ÐèÒª±ê×¢×´Ì¬µÄ¾ÛºÏ¸ùÁÐ±í¡£</param>
        public virtual async Task RegisterNew<TAggregateRoot>(IList<TAggregateRoot> objs)
            where TAggregateRoot : class, IAggregateRoot
        {
            if (objs != null && objs.Any())
            {
                foreach (var obj in objs)
                {
                    await RegisterNew(obj);
                }
            }
        }

        /// <summary>
        /// Registers a modified object to the repository context.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual async Task RegisterModified<TAggregateRoot>(TAggregateRoot obj) where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.IsTransient())
            {
                obj.GenerateId();
            }
            if (_localDeletedCollection.Value.ContainsKey(obj.Id))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!_localModifiedCollection.Value.ContainsKey(obj.Id) && !_localNewCollection.Value.ContainsKey(obj.Id))
                _localModifiedCollection.Value.Add(obj.Id, obj);
            _localCommitted.Value = false;

            await Task.CompletedTask;
        }

        /// <summary>
        /// ÅúÁ¿½«Ö¸¶¨µÄ¾ÛºÏ¸ù±ê×¢Îª¡°¸ü¸Ä¡±×´Ì¬¡£
        /// </summary>
        /// <typeparam name="TAggregateRoot">ÐèÒª±ê×¢×´Ì¬µÄ¾ÛºÏ¸ùÀàÐÍ¡£</typeparam>
        /// <param name="objs">ÐèÒª±ê×¢×´Ì¬µÄ¾ÛºÏ¸ùÁÐ±í¡£</param>
        public virtual async Task RegisterModified<TAggregateRoot>(IList<TAggregateRoot> objs)
            where TAggregateRoot : class, IAggregateRoot
        {
            if (objs != null && objs.Any())
            {
                foreach (var obj in objs)
                {
                    await RegisterModified(obj);
                }
            }
        }

        /// <summary>
        /// Registers a deleted object to the repository context.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual async Task RegisterDeleted<TAggregateRoot>(TAggregateRoot obj) where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.IsTransient())
            {
                obj.GenerateId();
            }
            if (_localNewCollection.Value.ContainsKey(obj.Id))
            {
                if (_localNewCollection.Value.Remove(obj.Id))
                    return;
            }
            bool removedFromModified = _localModifiedCollection.Value.Remove(obj.Id);
            bool addedToDeleted = false;
            if (!_localDeletedCollection.Value.ContainsKey(obj.Id))
            {
                _localDeletedCollection.Value.Add(obj.Id, obj);
                addedToDeleted = true;
            }
            _localCommitted.Value = !(removedFromModified || addedToDeleted);

            await Task.CompletedTask;
        }

        /// <summary>
        /// ÅúÁ¿½«Ö¸¶¨µÄ¾ÛºÏ¸ù±ê×¢Îª¡°É¾³ý¡±×´Ì¬¡£
        /// </summary>
        /// <typeparam name="TAggregateRoot">ÐèÒª±ê×¢×´Ì¬µÄ¾ÛºÏ¸ùÀàÐÍ¡£</typeparam>
        /// <param name="objs">ÐèÒª±ê×¢×´Ì¬µÄ¾ÛºÏ¸ùÁÐ±í¡£</param>
        public virtual async Task RegisterDeleted<TAggregateRoot>(IList<TAggregateRoot> objs)
            where TAggregateRoot : class, IAggregateRoot
        {
            if (objs != null && objs.Any())
            {
                foreach (var obj in objs)
                {
                    await RegisterDeleted(obj);
                }
            }
        }

        #endregion

        #region IUnitOfWork Members

        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates whether the UnitOfWork
        /// was committed.
        /// </summary>
        public bool Committed
        {
            get { return _localCommitted.Value; }
            protected set { _localCommitted.Value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void BeginTransaction();

        /// <summary>
        /// Commits the UnitOfWork.
        /// </summary>
        public abstract Task CommitAsync();

        /// <summary>
        /// Rolls-back the UnitOfWork.
        /// </summary>
        public abstract Task RollbackAsync();

        #endregion
    }
}