namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery
{
    /// <summary>
    /// 表示自定义查询所选择的字段
    /// </summary>
    public class CustomeQueryField  
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据类型（Int, Decimal, String....）
        /// </summary>
        public string DateType { get; set; }

        /// <summary>
        /// 字段类型（常规字段,自定义字段）
        /// </summary>
        public string FieldType { get; set; }
    }
}
