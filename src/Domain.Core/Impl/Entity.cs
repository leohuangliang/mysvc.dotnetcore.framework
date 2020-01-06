using System;

namespace MySvc.DotNetCore.Framework.Domain.Core.Impl
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            this.Id = string.Empty;
        }

        protected Entity(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
        public byte[] RowVersion { get; set; }

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