using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-字符串字段值与多个字符串集合值的关系的查询表达式
    /// </summary>
    public class CustomeQueryStringMultiMatchExpression : CustomeQueryMultiMatchExpression<string>
    {
        public CustomeQueryStringMultiMatchExpression()
        {
            Type = CustomeQueryExpressionType.STRING_MULTI_MATCH;
        }
    }
}
