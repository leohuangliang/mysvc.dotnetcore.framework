using System;

namespace MySvc.DotNetCore.Framework.Domain.Core.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        protected Entity()
        {
            this.Id = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected Entity(string id)
        {
            Id = id;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void GenerateId()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                this.Id = Guid.NewGuid().ToString();
            }
        }


        /// <summary>
        ///     Check if this entity is transient, ie, without identity at this moment
        /// </summary>
        /// <returns>True if entity is transient, else false</returns>
        public bool IsTransient()
        {
            return string.IsNullOrWhiteSpace(this.Id);
        }
    }
}