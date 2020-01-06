namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery
{
    /// <summary>
    ///  表示一个自定义查询的目标值（范围值）
    /// </summary>
    public class CustomeQueryRangeValue<T> : CustomeQueryValue
    {
        /// <summary>
        /// 范围左值
        /// </summary>
        public T LeftValue { get; set; }

        /// <summary>
        /// 范围右值
        /// </summary>
        public T RightValue { get; set; }
    }
}
