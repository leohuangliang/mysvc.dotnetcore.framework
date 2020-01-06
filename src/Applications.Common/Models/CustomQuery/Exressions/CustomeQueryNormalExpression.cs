using Capmarvel.Framework.Applications.Common.Models.CustomQuery;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-普通表达式(类似二元表达式)
    /// </summary>
    public abstract class CustomeQueryNormalExpression<T> : CustomeQueryExpression
    {
        /// <summary>
        /// 自定义查询的字段
        /// </summary>
        public CustomeQueryField Field { get; set; }

        /// <summary>
        /// 关系运算符（Equal, GreaterThan....）
        /// </summary>
        public string RelationalOperator { get; set; }

        /// <summary>
        /// 目标值
        /// </summary>
        public CustomeQuerySingleValue<T> Value { get; set; }
    }
}
