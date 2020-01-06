using System;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Common.Constants;
using Capmarvel.Framework.Domain.Common.ExpressionBuilders.CustomQuery;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-普通表达式(类似二元表达式)
    /// </summary>
    public abstract class CustomeQueryNormalExpression<T> : CustomeQueryExpression
    {
        protected CustomeQueryNormalExpression(CustomeQueryField field, string relationalOperator,
            CustomeQuerySingleValue<T> value, string type) : base(type)
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
        public CustomeQuerySingleValue<T> Value { get; private set; }

        public override Expression<Func<TK, bool>> GetExpression<TK>()
        {
            var expressionTreeBuilder = CustomQueryExpressionManager.GetLambdaExpressioneBuilder<TK>(Field);

            if (expressionTreeBuilder != null)
            {
                return expressionTreeBuilder.GetExpression(RelationalOperator, Value);
            }

            var pe = Expression.Parameter(typeof(TK), "x");

            var left = CustomQueryExpressionManager.GetFieldExpression<TK>(pe, Field);
            var right = Expression.Constant(Value.Value, typeof(T));

            Expression predicateBody = null;

            if (RelationalOperator == CustomeQueryRelationalOperator.EQUAL)
            {
                predicateBody = Expression.Equal(left, right);
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.NOT_EQUAL)
            {
                predicateBody = Expression.NotEqual(left, right);
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.GREATER_THAN)
            {
                predicateBody = Expression.GreaterThan(left, right);
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.GREATER_THAN_OR_EQUAL)
            {
                predicateBody = Expression.GreaterThanOrEqual(left, right);
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.LESS_THAN)
            {
                predicateBody = Expression.LessThan(left, right);
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.LESS_THAN_OR_EQUAL)
            {
                predicateBody = Expression.LessThanOrEqual(left, right);
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.INCLUDE)
            {
                predicateBody = Expression.Call(left, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), right);
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.NOT_INCLUDE)
            {
                predicateBody = Expression.Not(Expression.Call(left, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), right));
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.START_WITH)
            {
                predicateBody = Expression.Call(left, typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), right);
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.END_WITH)
            {
                predicateBody = Expression.Not(Expression.Call(left, typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }), right));
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.NULL_OR_EMPTY)
            {
                var nullExpress = Expression.Equal(left, Expression.Constant(null));
                var emptyExpress = Expression.Equal(right, Expression.Constant(string.Empty));
                predicateBody = Expression.OrElse(nullExpress, emptyExpress);
            }
            else if (RelationalOperator == CustomeQueryRelationalOperator.NOT_NULL_OR_EMPTY)
            {
                var notNullExpress = Expression.NotEqual(left, Expression.Constant(null));
                var notEmptyExpress = Expression.NotEqual(right, Expression.Constant(string.Empty));
                predicateBody = Expression.AndAlso(notNullExpress, notEmptyExpress);
            }

            return Expression.Lambda<Func<TK, bool>>(predicateBody, new ParameterExpression[] { pe });
        }
    }
}
