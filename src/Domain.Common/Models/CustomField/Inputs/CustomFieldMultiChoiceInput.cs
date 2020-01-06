using System.Collections.Generic;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomField.Inputs
{
    /// <summary>
    ///  自定义字段的多选选择控件
    /// </summary>
    public class CustomFieldMultiChoiceInput : CustomFieldInput
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public IList<string> DefaultValue { get; private set; }

        /// <summary>
        /// 可选择项
        /// </summary>
        public IList<string> Options { get; private set; }

        public CustomFieldMultiChoiceInput(IList<string> defaultValue, string tips, IList<string> options) 
            : base(CustomFieldInputType.MULTI_CHOICE, string.Join(",", defaultValue ?? new List<string>()) , tips)
        {
            DefaultValue = defaultValue;
            Options = options;
        }
    }
}
