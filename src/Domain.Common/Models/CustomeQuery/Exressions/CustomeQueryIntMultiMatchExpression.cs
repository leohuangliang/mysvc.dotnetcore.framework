using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-整数字段值与多个整数集合值的关系的查询表达式
    /// </summary>
    public class CustomeQueryIntMultiMatchExpression : CustomeQueryMultiMatchExpression<int>
    {
        public CustomeQueryIntMultiMatchExpression(CustomeQueryField field, string relationalOperator, CustomeQueryMultiValue<int> value)
            : base(field, relationalOperator, value, CustomeQueryExpressionType.INT_MULTI_MATCH)
        {
        }
    }
}
