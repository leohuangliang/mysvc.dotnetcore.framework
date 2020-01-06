namespace Capmarvel.Framework.Applications.Common.Models.CustomField.Inputs
{
    /// <summary>
    /// 自定义字段的输入控件
    /// </summary>
    public abstract class CustomFieldInput
    {
        /// <summary>
        /// 控件类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 默认值字符串形式表示
        /// </summary>
        public string DefaultValueString { get; set; }

        /// <summary>
        /// 操作提示
        /// </summary>
        public string Tips { get; set; }
    }
}
