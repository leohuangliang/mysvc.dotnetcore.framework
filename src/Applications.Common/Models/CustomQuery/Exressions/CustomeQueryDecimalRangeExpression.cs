using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-数字数据范围查询表达式（between and）
    /// </summary>
    public class CustomeQueryDecimalRangeExpression : CustomeQueryRangeExpression<decimal>
    {
        public CustomeQueryDecimalRangeExpression()
        {
            Type = CustomeQueryExpressionType.DECIMAL_RANGE;
        }
    }
}
