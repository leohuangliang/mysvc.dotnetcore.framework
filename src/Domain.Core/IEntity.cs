using System;

namespace MySvc.DotNetCore.Framework.Domain.Core
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
        /// <summary>
        /// 
        /// </summary>
        Byte[] RowVersion { get; set; }

        void GenerateId(IEntityIdGenerator entityIdGenerator);
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