using System;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-日期时间字段值与多个数值集合值的关系的查询表达式
    /// </summary>
    public class CustomeQueryDateTimeMultiMatchExpression : CustomeQueryMultiMatchExpression<DateTime>
    {
        public CustomeQueryDateTimeMultiMatchExpression(CustomeQueryField field, string relationalOperator, CustomeQueryMultiValue<DateTime> value) 
            : base(field, relationalOperator, value, CustomeQueryExpressionType.DATETIME_MULTI_MATCH)
        {
        }
    }
}
