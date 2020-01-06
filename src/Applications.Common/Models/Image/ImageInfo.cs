namespace Capmarvel.Framework.Applications.Common.Models.Image
{
    /// <summary>
    /// 表示一个图片信息
    /// </summary>
    public abstract class ImageInfo
    { 
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
    }
}
