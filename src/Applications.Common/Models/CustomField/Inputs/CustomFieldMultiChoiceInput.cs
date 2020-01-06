using System.Collections.Generic;

namespace Capmarvel.Framework.Applications.Common.Models.CustomField.Inputs
{
    /// <summary>
    ///  自定义字段的多选选择控件
    /// </summary>
    public class CustomFieldMultiChoiceInput : CustomFieldInput
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public IList<string> DefaultValue { get; set; }

        /// <summary>
        /// 可选择项
        /// </summary>
        public IList<string> Options { get; set; }
    }
}
