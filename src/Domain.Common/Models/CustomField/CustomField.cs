using Capmarvel.Framework.Domain.Core.Impl;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Capmarvel.Framework.Domain.Common.Models.CustomField
{
    /// <summary>
    /// 自定义字段
    /// 包含了其定义及其值
    /// </summary>
    public class CustomField : Entity
    {
        public CustomField(CustomFieldDefinition definition)
        {
            Definition = definition;
        }

        public CustomField(CustomFieldDefinition definition, string textValue) : this(definition)
        {
            TextValue = textValue;
            ValueForString = textValue;
        }

        public CustomField(CustomFieldDefinition definition, decimal decimalValue) : this(definition)
        {
            DecimalValue = decimalValue;
            ValueForString = decimalValue.ToString(CultureInfo.InvariantCulture);
        }

        public CustomField(CustomFieldDefinition definition, DateTime dateTimeValue) : this(definition)
        {
            DateTimeValue = dateTimeValue;
            ValueForString = dateTimeValue.ToString("YYYY-MM-dd HH:mm:ss");
        }

        public CustomField(CustomFieldDefinition definition, IList<string> mutiChoiceValues) : this(definition)
        {
            MutiChoiceValues = mutiChoiceValues ?? new List<string>();
            ValueForString = string.Join(",", MutiChoiceValues);
        }

        /// <summary>
        /// 字段的定义
        /// </summary>
        public CustomFieldDefinition Definition { get; private set; }

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
