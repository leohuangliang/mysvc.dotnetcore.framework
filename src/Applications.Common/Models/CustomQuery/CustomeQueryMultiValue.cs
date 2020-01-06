using System;
using System.Collections.Generic;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery
{
    /// <summary>
    ///  表示一个自定义查询的目标值（多值）
    /// </summary>
    public class CustomeQueryMultiValue<T> : CustomeQueryValue
    {
        public IList<T> Values { get; set; }
    }
}
