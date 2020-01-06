using System;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-时间数据查询表达式
    /// </summary>
    public class CustomeQueryDateTimeNormalExpression : CustomeQueryNormalExpression<DateTime>
    {
        public CustomeQueryDateTimeNormalExpression(CustomeQueryField field, string relationalOperator, CustomeQuerySingleValue<DateTime> value) 
            : base(field, relationalOperator, value, CustomeQueryExpressionType.DATETIME_MATCH)
        {
        }
    }
}
