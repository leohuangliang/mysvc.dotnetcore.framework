using Capmarvel.Framework.Domain.Common.Models.CustomField.Inputs;
using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models.CustomField
{
    /// <summary>
    /// 产品自定义字段-字段
    /// </summary>
    public class CustomFieldDefinition : Entity
    {
        public CustomFieldDefinition(string name, bool isRequired, CustomFieldInput fieldInput)
        {
            Name = name;
            IsRequired = isRequired;
            FieldInput = fieldInput;
        }

        /// <summary>
        /// 自定义字段名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 是否必填项
        /// </summary>
        public bool IsRequired { get; private set; }

        /// <summary>
        /// 自定义字段的输入控件
        /// </summary>
        public CustomFieldInput FieldInput { get; private set; }
    }
}
