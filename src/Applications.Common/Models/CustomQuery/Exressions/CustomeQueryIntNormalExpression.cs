using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-整型数据查询表达式(类似二元表达式)
    /// </summary>
    public class CustomeQueryIntNormalExpression : CustomeQueryNormalExpression<int>
    {
        public CustomeQueryIntNormalExpression()
        {
            Type = CustomeQueryExpressionType.INT_MATCH;
        }
    }
}
