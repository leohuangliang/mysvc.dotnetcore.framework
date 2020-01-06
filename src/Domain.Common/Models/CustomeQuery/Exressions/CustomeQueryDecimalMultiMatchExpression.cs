using System;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-数值字段值与多个数值集合值的关系的查询表达式
    /// </summary>
    public class CustomeQueryDecimalMultiMatchExpression : CustomeQueryMultiMatchExpression<decimal>
    {
        public CustomeQueryDecimalMultiMatchExpression(CustomeQueryField field, string relationalOperator, CustomeQueryMultiValue<decimal> value) 
            : base(field, relationalOperator, value, CustomeQueryExpressionType.DECIMAL_MULTI_MATCH)
        {
        }
    }
}
