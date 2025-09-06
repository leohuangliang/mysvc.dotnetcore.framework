using System;
using System.Linq;

namespace MySvc.Framework.Domain.Core.Impl
{
    /// <summary>
    /// 泛型值对象
    /// </summary>
    /// <typeparam name="TValueObject"></typeparam>
    [Serializable]
    public class ValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : ValueObject<TValueObject>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TValueObject? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            //compare all public properties
            var publicProperties = GetType().GetProperties();

            if (publicProperties != null
                &&
                publicProperties.Any())
            {
                return publicProperties.All(p =>
                {
                    object? left = p.GetValue(this, null);
                    object? right = p.GetValue(other, null);

                    if (Equals(left, null))
                        return (Equals(right, null));

                    if (left is TValueObject)
                    {
                        //check not self-references...
                        return ReferenceEquals(left, right);
                    }
                    return left.Equals(right);
                });
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            ValueObject<TValueObject>? item = obj as ValueObject<TValueObject>;

            if (item is not null)
                return Equals((TValueObject)item!);
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 31;
            var changeMultiplier = false;
            const int index = 1;

            //compare all public properties
            var publicProperties = GetType().GetProperties();


            if (publicProperties != null
                &&
                publicProperties.Any())
            {
                foreach (var item in publicProperties)
                {
                    var value = item.GetValue(this, null);

                    if (value != null)
                    {

                        hashCode = hashCode * ((changeMultiplier) ? 59 : 114) + value.GetHashCode();

                        changeMultiplier = !changeMultiplier;
                    }
                    else
                    {
                        //only for support {"a",null,null,"a"} <> {null,"a","a",null}
                        hashCode = hashCode ^ (index * 13);
                    }
                }
            }

            return hashCode;
        }

        /// <summary>
        /// 相等操作符，支持可空类型
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        /// <returns>如果两个值对象相等则返回true，否则返回false</returns>
        public static bool operator ==(ValueObject<TValueObject>? left, ValueObject<TValueObject>? right)
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
        /// <returns>如果两个值对象不相等则返回true，否则返回false</returns>
        public static bool operator !=(ValueObject<TValueObject>? left, ValueObject<TValueObject>? right)
        {
            return !(left == right);
        }
    }
}