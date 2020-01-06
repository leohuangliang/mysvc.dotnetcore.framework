namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery.Exressions
{
    /// <summary>
    /// 自定义查询-字段值与多个字段对应类型的值列表的关系的查询表达式（如in， not in等）
    /// </summary>
    /// <typeparam name="T">字段数据类型</typeparam>
    public abstract class CustomeQueryMultiMatchExpression<T> : CustomeQueryExpression
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
        public CustomeQueryMultiValue<T> Value { get; set; }
    }
}
