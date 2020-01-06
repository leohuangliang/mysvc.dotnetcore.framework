using System;
using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-时间范围查询表达式（between and 或者 Not between and）
    /// </summary>
    public class CustomeQueryDateTimeRangeExpression : CustomeQueryRangeExpression<DateTime>
    {
        public CustomeQueryDateTimeRangeExpression()
        {
            Type = CustomeQueryExpressionType.DATETIME_RANGE;
        }
    }
}
