using System;
using System.Globalization;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomField.Inputs
{
    /// <summary>
    ///  自定义字段的日期时间控件
    /// </summary>
    public class CustomFieldDatetimeInput : CustomFieldInput
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public DateTime DefaultValue { get; private set; }
            
        public CustomFieldDatetimeInput(DateTime defaultValue, string tips)
            : base(CustomFieldInputType.DATETIME, defaultValue.ToString(CultureInfo.InvariantCulture), tips)
        {
            DefaultValue = defaultValue;
        }
    }
}
