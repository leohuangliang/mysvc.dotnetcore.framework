#nullable enable
using System;
using System.Linq.Expressions;

namespace MySvc.Framework.Domain.Core.Specification
{
    /// <summary>
    /// 表示"总不满足"的规范，用于处理特殊的null规范情况
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    public class NoneSpecification<T> : Specification<T>
    {
        /// <summary>
        /// 获取表示"总是false"的表达式
        /// </summary>
        /// <returns>总是返回false的LINQ表达式</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            return x => false;
        }

        /// <summary>
        /// 检查对象是否满足规范（总是返回false）
        /// </summary>
        /// <param name="obj">要检查的对象</param>
        /// <returns>总是返回false</returns>
        public override bool IsSatisfiedBy(T obj)
        {
            return false;
        }
    }
}
