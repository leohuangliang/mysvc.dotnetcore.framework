using Capmarvel.Framework.Applications.Common.Models.CustomField.Inputs;

namespace Capmarvel.Framework.Applications.Common.Models.CustomField
{
    /// <summary>
    /// 产品自定义字段-字段
    /// </summary>
    public class CustomFieldDefinition
    {
        /// <summary>
        /// 自定义字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否必填项
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 自定义字段的输入控件
        /// </summary>
        public CustomFieldInput FieldInput { get; set; }
    }
}
