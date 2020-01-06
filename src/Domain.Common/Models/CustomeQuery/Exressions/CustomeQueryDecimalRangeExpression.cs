using System;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-数字数据范围查询表达式（between and）
    /// </summary>
    public class CustomeQueryDecimalRangeExpression : CustomeQueryRangeExpression<decimal>
    {
        public CustomeQueryDecimalRangeExpression(CustomeQueryField field, string relationalOperator, CustomeQueryRangeValue<decimal> value) 
            : base(field, relationalOperator, value, CustomeQueryExpressionType.DECIMAL_RANGE)
        {
        }
    }
}
