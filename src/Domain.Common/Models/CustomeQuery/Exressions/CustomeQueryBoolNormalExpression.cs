using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions
{
    /// <summary>
    /// 自定义查询-布尔值数据查询表达式
    /// </summary>
    public class CustomeQueryBoolNormalExpression : CustomeQueryNormalExpression<bool>
    {
        public CustomeQueryBoolNormalExpression(CustomeQueryField field, string relationalOperator, CustomeQuerySingleValue<bool> value) 
            : base(field, relationalOperator, value, CustomeQueryExpressionType.BOOL_MATCH)
        {
        }
    }
}
