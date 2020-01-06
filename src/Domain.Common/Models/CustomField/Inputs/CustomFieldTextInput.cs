using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomField.Inputs
{
    /// <summary>
    ///  自定义字段的文本输入控件
    /// </summary>
    public class CustomFieldTextInput : CustomFieldInput
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; private set; }
            
        public CustomFieldTextInput(string defaultValue, string tips)
            : base(CustomFieldInputType.TEXT, defaultValue, tips)
        {
            DefaultValue = defaultValue;
        }
    }
}
