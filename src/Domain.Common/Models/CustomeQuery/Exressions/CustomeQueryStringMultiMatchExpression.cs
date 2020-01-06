using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-字符串字段值与多个字符串集合值的关系的查询表达式
    /// </summary>
    public class CustomeQueryStringMultiMatchExpression : CustomeQueryMultiMatchExpression<string>
    {
        public CustomeQueryStringMultiMatchExpression(CustomeQueryField field, string relationalOperator, CustomeQueryMultiValue<string> value) 
            : base(field, relationalOperator, value, CustomeQueryExpressionType.STRING_MULTI_MATCH)
        {
        }
    }
}
