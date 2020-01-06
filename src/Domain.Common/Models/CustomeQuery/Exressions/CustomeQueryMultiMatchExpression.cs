using System;
using System.Linq;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Common.Constants;
using Capmarvel.Framework.Domain.Common.ExpressionBuilders.CustomQuery;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-字段值与多个字段对应类型的值列表的关系的查询表达式（如in， not in等）
    /// </summary>
    /// <typeparam name="T">字段数据类型</typeparam>
    public abstract class CustomeQueryMultiMatchExpression<T> : CustomeQueryExpression
    {
        protected CustomeQueryMultiMatchExpression(CustomeQueryField field, string relationalOperator, CustomeQueryMultiValue<T> value, string type) : base(type)
        {
            Field = field;
            RelationalOperator = relationalOperator;
            Value = value;
        }

        /// <summary>
        /// 自定义查询的字段
        /// </summary>
        public CustomeQueryField Field { get; private set; }

        /// <summary>
        /// 关系运算符（Equal, GreaterThan....）
        /// </summary>
        public string RelationalOperator { get; private set; }

        /// <summary>
        /// 值
        /// </summary>
        public CustomeQueryMultiValue<T> Value { get; private set; }

        public override Expression<Func<TK, bool>> GetExpression<TK>()
        {
            var expressionTreeBuilder = CustomQueryExpressionManager.GetLambdaExpressioneBuilder<TK>(Field);

            if (expressionTreeBuilder != null)
            {
                return expressionTreeBuilder.GetExpression(RelationalOperator, Value);
            }

            var pe = Expression.Parameter(typeof(TK), "x");

            var valuesConstant = Expression.Constant(Value.Values.ToList());
            var fieldExpression = CustomQueryExpressionManager.GetFieldExpression<TK>(pe, Field);

            var containsCallExpression = Expression.Call(
                typeof(Enumerable),
                "Contains",
                new Type[] { typeof(string) },
                valuesConstant,
                fieldExpression);

            Expression predicateBody = containsCallExpression;

            //如果是 不等于以下任一
            if (RelationalOperator == CustomeQueryRelationalOperator.NOT_CONTAINS)
            {
                predicateBody = Expression.Not(containsCallExpression);
            }

            return Expression.Lambda<Func<TK, bool>>(predicateBody, new ParameterExpression[] { pe });
        }
    }
}
