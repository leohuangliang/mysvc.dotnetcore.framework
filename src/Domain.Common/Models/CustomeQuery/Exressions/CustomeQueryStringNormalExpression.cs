using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-字符串数据查询表达式(类似二元表达式)
    /// </summary>
    public class CustomeQueryStringNormalExpression : CustomeQueryNormalExpression<string>
    {
        public CustomeQueryStringNormalExpression(CustomeQueryField field, string relationalOperator, CustomeQuerySingleValue<string> value)
            : base(field, relationalOperator, value, CustomeQueryExpressionType.STRING_MATCH)
        {
        }
    }
}
