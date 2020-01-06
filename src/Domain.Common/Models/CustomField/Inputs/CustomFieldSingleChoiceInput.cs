using System.Collections.Generic;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomField.Inputs
{
    /// <summary>
    ///  自定义字段的单选选择控件
    /// </summary>
    public class CustomFieldSingleChoiceInput : CustomFieldInput
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; private set; }
        
        /// <summary>
        /// 可选择项
        /// </summary>
        public IList<string> Options { get; private set; }

        public CustomFieldSingleChoiceInput(string defaultValue, string tips, IList<string> options)
            : base(CustomFieldInputType.SINGLE_CHOICE, defaultValue, tips)
        {
            DefaultValue = defaultValue;
            Options = options;
        }
    }
}
