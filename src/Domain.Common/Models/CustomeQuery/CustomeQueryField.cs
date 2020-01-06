using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models.CustomeQuery
{
    /// <summary>
    /// 表示自定义查询所选择的字段
    /// </summary>
    public class CustomeQueryField : ValueObject<CustomeQueryField>
    {
        public CustomeQueryField(string name, string dateType, string fieldType)
        {
            Name = name;
            DateType = dateType;
            FieldType = fieldType;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DateType { get; private set; }

        /// <summary>
        /// 字段类型（常规字段,自定义字段）
        /// </summary>
        public string FieldType { get; private set; }
    }
}
