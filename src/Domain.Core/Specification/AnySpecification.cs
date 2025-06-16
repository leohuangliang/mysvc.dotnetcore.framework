#nullable enable
using System;
using System.Linq.Expressions;

namespace MySvc.Framework.Domain.Core.Specification
{
    /// <summary>
    /// 表示"总是满足"的规范，用于处理null规范的默认情况
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    public class AnySpecification<T> : Specification<T>
    {
        /// <summary>
        /// 获取表示"总是true"的表达式
        /// </summary>
        /// <returns>总是返回true的LINQ表达式</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            return x => true;
        }

        /// <summary>
        /// 检查对象是否满足规范（总是返回true）
        /// </summary>
        /// <param name="obj">要检查的对象</param>
        /// <returns>总是返回true</returns>
        public override bool IsSatisfiedBy(T obj)
        {
            return true;
        }
    }
}
