using System;
using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-日期时间字段值与多个数值集合值的关系的查询表达式
    /// </summary>
    public class CustomeQueryDateTimeMultiMatchExpression : CustomeQueryMultiMatchExpression<DateTime>
    {
        public CustomeQueryDateTimeMultiMatchExpression()
        {
            Type = CustomeQueryExpressionType.DATETIME_MULTI_MATCH;
        }
    }
}
