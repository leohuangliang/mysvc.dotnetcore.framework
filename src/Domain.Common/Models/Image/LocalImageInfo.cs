using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.Image
{
    /// <summary>
    /// 本地云存储文件
    /// </summary>
    public class LocalImageInfo : ImageInfo
    {
        public LocalImageInfo(string imageId, int sort = 0) : base(ImageInfoType.LOCAL, sort)
        {
            ImageId = imageId;
            Sort = sort;
        }

        /// <summary>
        /// 图片文件的Id
        /// </summary>
        public string ImageId { get; private set; }
    }
}
