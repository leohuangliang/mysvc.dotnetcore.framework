using System.ComponentModel;

namespace MySvc.Framework.Domain.Core.Paged
{
    /// <summary>
    /// 排序顺序
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// 升序
        /// </summary>
        [Description("升序")]
        Ascending = 0,

        /// <summary>
        /// 降序
        /// </summary>
        [Description("降序")]
        Descending = 1
    }
}
