using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-布尔值数据查询表达式(类似二元表达式)
    /// </summary>
    public class CustomeQueryBoolNormalExpression : CustomeQueryNormalExpression<bool>
    {
        public CustomeQueryBoolNormalExpression()
        {
            Type = CustomeQueryExpressionType.BOOL_MATCH;
        }
    }
}
