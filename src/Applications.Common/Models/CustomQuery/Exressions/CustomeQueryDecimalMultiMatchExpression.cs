using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-数值字段值与多个数值集合值的关系的查询表达式
    /// </summary>
    public class CustomeQueryDecimalMultiMatchExpression : CustomeQueryMultiMatchExpression<decimal>
    {
        public CustomeQueryDecimalMultiMatchExpression()
        {
            Type = CustomeQueryExpressionType.DECIMAL_MULTI_MATCH;
        }
    }
}
