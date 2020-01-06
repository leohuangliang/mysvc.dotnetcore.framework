using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-整型数据范围查询表达式
    /// </summary>
    public class CustomeQueryIntRangeExpression : CustomeQueryRangeExpression<int>
    {
        public CustomeQueryIntRangeExpression(CustomeQueryField field, string relationalOperator, CustomeQueryRangeValue<int> value)
            : base(field, relationalOperator, value, CustomeQueryExpressionType.INT_RANGE)
        {
        }
    }
}
