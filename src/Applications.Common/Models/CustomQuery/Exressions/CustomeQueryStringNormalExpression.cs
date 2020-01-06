using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-字符串数据查询表达式(类似二元表达式)
    /// </summary>
    public class CustomeQueryStringNormalExpression : CustomeQueryNormalExpression<string>
    {
        public CustomeQueryStringNormalExpression()
        {
            Type = CustomeQueryExpressionType.STRING_MATCH;
        }
    }
}
