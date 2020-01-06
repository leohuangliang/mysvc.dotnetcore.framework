using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models.CustomField.Inputs
{
    /// <summary>
    /// 自定义字段的输入控件
    /// </summary>
    public abstract class CustomFieldInput : ValueObject<CustomFieldInput>
    {
        protected CustomFieldInput(string type, string defaultValueString, string tips)
        {
            Type = type;
            Tips = tips;

            DefaultValueString = defaultValueString;
        }

        /// <summary>
        /// 控件类型
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// 默认值字符串形式表示
        /// </summary>
        public string DefaultValueString { get; private set; }

        /// <summary>
        /// 操作提示
        /// </summary>
        public string Tips { get; private set; }
    }
}
