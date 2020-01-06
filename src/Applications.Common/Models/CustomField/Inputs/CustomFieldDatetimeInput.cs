using System;

namespace Capmarvel.Framework.Applications.Common.Models.CustomField.Inputs
{
    /// <summary>
    ///  自定义字段的日期时间控件
    /// </summary>
    public class CustomFieldDatetimeInput : CustomFieldInput
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public DateTime DefaultValue { get; set; }           
    }
}
