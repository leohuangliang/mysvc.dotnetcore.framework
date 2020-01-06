using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models.Image
{
    /// <summary>
    /// 表示一个图片信息
    /// </summary>
    public abstract class ImageInfo : ValueObject<ImageInfo>
    {
        private ImageInfo()
        {

        }

        protected ImageInfo(string type, int sort)
        {
            this.Type = type;
            this.Sort = sort;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
    }
}
