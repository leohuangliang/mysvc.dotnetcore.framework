namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery
{
    /// <summary>
    ///  表示一个自定义查询的目标值（范围值）
    /// </summary>
    public class CustomeQueryRangeValue<T> : CustomeQueryValue
    {
        public CustomeQueryRangeValue(T leftValue, T rightValue)
        {
            LeftValue = leftValue;
            RightValue = rightValue;
        }

        /// <summary>
        /// 范围左值
        /// </summary>
        public T LeftValue { get; private set; }

        /// <summary>
        /// 范围右值
        /// </summary>
        public T RightValue { get; private set; }
    }
}
