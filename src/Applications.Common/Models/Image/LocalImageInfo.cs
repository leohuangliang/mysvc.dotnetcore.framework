using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.Image
{
    /// <summary>
    /// 本地云存储文件
    /// </summary>
    public class LocalImageInfo : ImageInfo
    {
        /// <summary>
        /// </summary>
        public LocalImageInfo()
        {
            Type = ImageInfoType.LOCAL;
        }

        /// <summary>
        /// 图片文件的Id
        /// </summary>
        public string ImageId { get; set; }
    }
}
