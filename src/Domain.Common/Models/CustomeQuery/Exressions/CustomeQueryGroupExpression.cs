using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询组表达式（聚合其他表达式）
    /// </summary>
    public class CustomeQueryGroupExpression : CustomeQueryExpression
    {
        public CustomeQueryGroupExpression(IList<CustomeQueryExpression> expressions, IList<string> logicalOperators)
            : base(CustomeQueryExpressionType.GROUP)
        {
            Expressions = expressions;
            LogicalOperators = logicalOperators;
        }

        /// <summary>
        /// 自定义查询表达式列表
        /// </summary>
        public IList<CustomeQueryExpression> Expressions { get; private set; }

        /// <summary>
        /// 逻辑运算符（与或非），用于连接Expressions的。 其数量应为 Expressions的数量 - 1
        /// </summary>
        public IList<string> LogicalOperators { get; private set; }

        public override Expression<Func<T, bool>> GetExpression<T>()
        {
            var megerExpression = Expressions[0].GetExpression<T>();

            for (int i = 1; i < Expressions.Count; i++)
            {
                var first = megerExpression;
                var second = Expressions[i].GetExpression<T>();
                var logicalOperator = LogicalOperators[i - 1];

                megerExpression = logicalOperator == CustomeQueryLogicalOperator.AND ?
                    Compose(first, second, Expression.And) : Compose(first, second, Expression.Or);
            }

            return megerExpression;
        }

        private Expression<TF> Compose<TF>(Expression<TF> first, Expression<TF> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<TF>(merge(first.Body, secondBody), first.Parameters);
        }

        private class ParameterRebinder : ExpressionVisitor
        {
            #region Private Fields
            private readonly Dictionary<ParameterExpression, ParameterExpression> _map;
            #endregion

            #region Ctor
            internal ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this._map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }
            #endregion

            #region Internal Static Methods
            internal static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }
            #endregion

            #region Protected Methods
            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;
                if (_map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
            #endregion
        }
    }
}
