using System;

namespace MySvc.Framework.Domain.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        TKey Id { get; }
        ///// <summary>
        ///// 
        ///// </summary>
        //[Obsolete("已废弃，请使用Timestamp字段")]
        //Byte[] RowVersion { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        string Timestamp { get; set; }

        /// <summary>
        /// 设置ID
        /// </summary>
        /// <param name="key"></param>
        void SetId(TKey key);

        //void GenerateId(IEntityIdGenerator entityIdGenerator);
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IEntity : IEntity<string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsTransient();
    }
}