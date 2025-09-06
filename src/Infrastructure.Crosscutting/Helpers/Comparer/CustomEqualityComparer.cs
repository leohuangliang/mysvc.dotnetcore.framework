using System;
using System.Collections.Generic;

namespace MySvc.Framework.Infrastructure.Crosscutting.Helpers.Comparer
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
        
        public bool Equals(T? x, T? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            if (obj == null) return 0;
            var key = keySelector(obj);
            return key == null ? 0 : comparer.GetHashCode(key);
        }
    }
}