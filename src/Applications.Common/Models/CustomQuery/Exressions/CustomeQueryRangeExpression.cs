namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-范围查询表达式（between and 或者 not between and）
    /// </summary>
    public abstract class CustomeQueryRangeExpression<T> : CustomeQueryExpression
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
        /// 范围值
        /// </summary>
        public CustomeQueryRangeValue<T> Value { get; set; }
    }
}
