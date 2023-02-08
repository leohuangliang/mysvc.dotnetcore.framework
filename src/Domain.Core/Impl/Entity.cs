using System;

namespace MySvc.Framework.Domain.Core.Impl
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
        ///// <summary>
        ///// 
        ///// </summary>
        //[Obsolete("已废弃，请使用Timestamp字段")]
        //public byte[] RowVersion { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 设置ID
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetId(string key)
        {
            this.Id = key ?? throw new ArgumentNullException(nameof(key));
        }


        //public void GenerateId(IEntityIdGenerator entityIdGenerater)
        //{
        //    if (entityIdGenerater is null)
        //    {
        //        throw new ArgumentNullException(nameof(entityIdGenerater));
        //    }

        //    this.Id = entityIdGenerater.GenerateId();
        //}


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