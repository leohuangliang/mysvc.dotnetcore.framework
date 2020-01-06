using System;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Common.Models.CustomeQuery;

namespace Capmarvel.Framework.Domain.Common.ExpressionBuilders.CustomQuery
{
    /// <summary>
    /// 自定义查询-构建一个表示聚合对象的字段表达式的构建器
    /// （eg: x.Name, x.CreateOperation.Time）
    /// </summary>
    public interface ICustomQueryFieldExpressionBuilder
    {
        /// <summary>
        /// 字段信息
        /// </summary>
        CustomeQueryField Field { get; }

        /// <summary>
        /// 聚合根对象的类型
        /// </summary>
        Type AggregateRootType { get; }

        /// <summary>
        /// 表示聚合对象的字段的表达式
        /// </summary>
        /// <param name="parameterExpression">变量参数表达式， 表示x.Name的x</param>
        /// <returns>表达式</returns>
        Expression GetFieldExpression(ParameterExpression parameterExpression);
    }
}
