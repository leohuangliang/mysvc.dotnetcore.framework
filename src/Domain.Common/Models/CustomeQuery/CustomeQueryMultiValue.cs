using System.Collections.Generic;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery
{
    /// <summary>
    ///  表示一个自定义查询的目标值（多值）
    /// </summary>
    public class CustomeQueryMultiValue<T> : CustomeQueryValue
    {
        public CustomeQueryMultiValue(IList<T> values)
        {
            Values = values;
        }

        public IList<T> Values { get; private set; }
    }
}
