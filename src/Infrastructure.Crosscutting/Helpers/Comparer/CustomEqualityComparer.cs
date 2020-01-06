using System;
using System.Collections.Generic;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers.Comparer
{
    /// <summary>
    /// 自定义的相等比较器
    /// </summary>
    public class CustomEqualityComparer<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;
        private IEqualityComparer<V> comparer;

        /// <summary>
        /// </summary>
        public CustomEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }
        
        /// <summary>
        /// </summary>
        public CustomEqualityComparer(Func<T, V> keySelector) : this(keySelector, EqualityComparer<V>.Default)
        {             
        }
        
        public bool Equals(T x, T y)
        {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(keySelector(obj));
        }
    }
}