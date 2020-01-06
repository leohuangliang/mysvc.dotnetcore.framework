using System;
using System.Collections.Generic;

namespace Capmarvel.Framework.Applications.Common.Models.CustomField
{
    /// <summary>
    /// 自定义字段
    /// 包含了其定义及其值
    /// </summary>
    public class CustomField
    {
        /// <summary>
        /// 字段的定义
        /// </summary>
        public CustomFieldDefinition Definition { get; set; }

        /// <summary>
        /// 字段值对应的字符串表示
        /// </summary>
        public string ValueForString { get; set; }

        /// <summary>
        /// 文本值
        /// </summary>
        public string TextValue { get; set; }

        /// <summary>
        /// 数字值
        /// </summary>
        public decimal DecimalValue { get; set; }

        /// <summary>
        /// 时间值
        /// </summary>
        public DateTime? DateTimeValue { get; set; }

        /// <summary>
        /// 多选值
        /// </summary>
        public IList<string> MutiChoiceValues { get; set; }
    }
}
