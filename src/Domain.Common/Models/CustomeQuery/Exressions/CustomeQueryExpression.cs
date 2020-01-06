using System;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询表达式
    /// </summary>
    public abstract class CustomeQueryExpression
    {
        protected CustomeQueryExpression(string type)
        {
            Type = type;
        }

        /// <summary>
        /// 表达式类型
        /// </summary>
        public string Type { get; private set; }

        public abstract Expression<Func<T, bool>> GetExpression<T>() where T : AggregateRoot;
    }
}

