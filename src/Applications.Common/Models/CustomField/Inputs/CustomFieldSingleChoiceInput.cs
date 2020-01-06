using System.Collections.Generic;

namespace Capmarvel.Framework.Applications.Common.Models.CustomField.Inputs
{
    /// <summary>
    ///  自定义字段的单选选择控件
    /// </summary>
    public class CustomFieldSingleChoiceInput : CustomFieldInput
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        
        /// <summary>
        /// 可选择项
        /// </summary>
        public IList<string> Options { get; set; }
    }
}
