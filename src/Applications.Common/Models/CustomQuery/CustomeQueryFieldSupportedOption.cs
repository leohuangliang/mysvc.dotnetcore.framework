using System.Collections.Generic;

namespace Capmarvel.Framework.Applications.Common.Models.CustomQuery
{
    /// <summary>
    /// 自定义查询中，字段可支持的操作选项
    /// </summary>
    public class CustomeQueryFieldSupportedOption
    {
        /// <summary>
        /// 字段信息
        /// </summary>
        public CustomeQueryField Field { get; set; }

        /// <summary>
        /// 可以支持关系运算符
        /// </summary>
        public IList<string> SupportedRelationalOperators { get; set; }
    }
}
