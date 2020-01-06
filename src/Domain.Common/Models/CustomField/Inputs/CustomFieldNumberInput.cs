using System.Globalization;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.CustomField.Inputs
{
    /// <summary>
    ///  自定义字段的数字输入控件
    /// </summary>
    public class CustomFieldNumberInput : CustomFieldInput
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public decimal DefaultValue { get; private set; }
            
        public CustomFieldNumberInput(decimal defaultValue, string tips)
            : base(CustomFieldInputType.NUMBER, defaultValue.ToString(CultureInfo.InvariantCulture), tips)
        {
            DefaultValue = defaultValue;
        }
    }
}
