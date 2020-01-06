using System;

namespace MySvc.DotNetCore.Framework.Domain.Core
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
        Byte[] RowVersion { get; set; }
        void GenerateId();
    }
    public interface IEntity : IEntity<string>
    {
        bool IsTransient();
    }
}