using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-数字类型数据查询表达式(类似二元表达式)
    /// </summary>
    public class CustomeQueryDecimalNormalExpression : CustomeQueryNormalExpression<decimal>
    {
        public CustomeQueryDecimalNormalExpression()
        {
            Type = CustomeQueryExpressionType.DECIMAL_MATCH;
        }
    }
}
