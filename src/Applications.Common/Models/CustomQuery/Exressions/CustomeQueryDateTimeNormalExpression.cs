using System;
using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-时间数据查询表达式(类似二元表达式)
    /// </summary>
    public class CustomeQueryDateTimeNormalExpression : CustomeQueryNormalExpression<DateTime>
    {
        public CustomeQueryDateTimeNormalExpression()
        {
            Type = CustomeQueryExpressionType.DATETIME_MATCH;
        }
    }
}
