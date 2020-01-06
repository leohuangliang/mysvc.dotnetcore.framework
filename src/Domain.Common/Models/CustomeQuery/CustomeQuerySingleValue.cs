namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery
{
    /// <summary>
    ///  表示一个自定义查询的目标值（单值）
    /// </summary>
    public class CustomeQuerySingleValue<T> : CustomeQueryValue
    {
        public CustomeQuerySingleValue(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
