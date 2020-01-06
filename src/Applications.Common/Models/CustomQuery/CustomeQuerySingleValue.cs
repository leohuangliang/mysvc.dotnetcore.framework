namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery
{
    /// <summary>
    ///  表示一个自定义查询的目标值（单值）
    /// </summary>
    public class CustomeQuerySingleValue<T> : CustomeQueryValue
    {
        public T Value { get; set; }
    }
}
