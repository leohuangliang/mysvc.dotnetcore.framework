using System;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-时间范围查询表达式（between and 或者 Not between and）
    /// </summary>
    public class CustomeQueryDateTimeRangeExpression : CustomeQueryRangeExpression<DateTime>
    {
        public CustomeQueryDateTimeRangeExpression(CustomeQueryField field, string relationalOperator, CustomeQueryRangeValue<DateTime> value)
            : base(field, relationalOperator, value, CustomeQueryExpressionType.DATETIME_RANGE)
        {
        }
    }
}
