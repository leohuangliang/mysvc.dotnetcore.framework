using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-整型数据查询表达式
    /// </summary>
    public class CustomeQueryIntNormalExpression : CustomeQueryNormalExpression<int>
    {
        public CustomeQueryIntNormalExpression(CustomeQueryField field, string relationalOperator, CustomeQuerySingleValue<int> value) 
            : base(field, relationalOperator, value, CustomeQueryExpressionType.INT_MATCH)
        {
        }
    }
}
