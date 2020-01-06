using System.Collections.Generic;
using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询组表达式（聚合其他表达式）
    /// </summary>
    public class CustomeQueryGroupExpression : CustomeQueryExpression
    {
        public CustomeQueryGroupExpression()
        {
            Type = CustomeQueryExpressionType.GROUP;
        }

        /// <summary>
        /// 自定义查询表达式列表
        /// </summary>
        public IList<CustomeQueryExpression> Expressions { get; set; }

        /// <summary>
        /// 逻辑运算符（与或非），用于连接Expressions的。 其数量应为 Expressions的数量 - 1
        /// </summary>
        public IList<string> LogicalOperators { get; set; }
    }
}
