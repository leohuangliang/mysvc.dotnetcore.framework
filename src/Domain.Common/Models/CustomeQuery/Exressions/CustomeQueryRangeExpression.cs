using System;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Common.Constants;
using Capmarvel.Framework.Domain.Common.ExpressionBuilders.CustomQuery;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-范围查询表达式（between and 或者 not between and）
    /// </summary>
    public abstract class CustomeQueryRangeExpression<T> : CustomeQueryExpression
    {
        protected CustomeQueryRangeExpression(CustomeQueryField field, string relationalOperator, CustomeQueryRangeValue<T> value, string type) : base(type)
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
        /// 范围值
        /// </summary>
        public CustomeQueryRangeValue<T> Value { get; private set; }

        public override Expression<Func<TK, bool>> GetExpression<TK>()
        {
            var expressionTreeBuilder = CustomQueryExpressionManager.GetLambdaExpressioneBuilder<TK>(Field);

            if (expressionTreeBuilder != null)
            {
                return expressionTreeBuilder.GetExpression(RelationalOperator, Value);
            }

            var pe = Expression.Parameter(typeof(TK), "x");

            //范围左值
            var left = CustomQueryExpressionManager.GetFieldExpression<TK>(pe, Field);
            var right = Expression.Constant(Value.LeftValue, typeof(T));
            var e1 = RelationalOperator == CustomeQueryRelationalOperator.BETWEEN ?
                Expression.GreaterThanOrEqual(left, right) : Expression.LessThan(left, right);

            //范围右值
            left = CustomQueryExpressionManager.GetFieldExpression<TK>(pe, Field);
            right = Expression.Constant(Value.RightValue, typeof(T));
            var e2 = RelationalOperator == CustomeQueryRelationalOperator.BETWEEN ?
                Expression.LessThanOrEqual(left, right) : Expression.GreaterThan(left, right);

            var predicateBody = Expression.AndAlso(e1, e2);
            return Expression.Lambda<Func<TK, bool>>(predicateBody, new ParameterExpression[] { pe });
        }
    }
}
