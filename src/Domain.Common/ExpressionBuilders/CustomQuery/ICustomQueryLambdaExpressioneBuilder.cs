using System;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Common.Models.CustomeQuery;
using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.ExpressionBuilders.CustomQuery
{
    /// <summary>
    /// 自定义查询转系统lambda表达式树的构建器
    /// </summary>
    public interface ICustomQueryLambdaExpressioneBuilder
    {
        /// <summary>
        /// 聚合根对象的类型
        /// </summary>
        Type AggregateRootType { get; }

        /// <summary>
        /// 字段信息
        /// </summary>
        CustomeQueryField Field { get; }
    }

    /// <summary>
    /// 自定义查询转系统lambda表达式树的构建器
    /// </summary>
    /// <typeparam name="T">聚合根类型</typeparam>
    public interface ICustomQueryLambdaExpressioneBuilder<T> : ICustomQueryLambdaExpressioneBuilder where T : AggregateRoot
    {    
        /// <summary>
        /// 根据自定义查询表达式对象，构建系统lambda表达式树对象
        /// </summary>
        /// <typeparam name="T">聚合根类型</typeparam>
        /// <param name="relationalOperator">关系运算符（Equal, GreaterThan....）</param>
        /// <param name="value">关系比较的值</param>
        /// <returns>系统lambda表达式树对象</returns>
        Expression<Func<T, bool>> GetExpression(string relationalOperator, CustomeQueryValue value);
    }
}
