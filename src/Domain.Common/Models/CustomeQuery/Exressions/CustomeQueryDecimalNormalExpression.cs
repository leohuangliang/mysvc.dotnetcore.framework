using System;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-数字类型数据查询表达式
    /// </summary>
    public class CustomeQueryDecimalNormalExpression : CustomeQueryNormalExpression<decimal>
    {
        public CustomeQueryDecimalNormalExpression(CustomeQueryField field, string relationalOperator, CustomeQuerySingleValue<decimal> value)
            : base(field, relationalOperator, value, CustomeQueryExpressionType.DECIMAL_MATCH)
        {
        }
    }
}
