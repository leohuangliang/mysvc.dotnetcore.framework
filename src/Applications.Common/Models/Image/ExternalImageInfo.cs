using Capmarvel.Framework.Applications.Common.Constants;

namespace Capmarvel.Framework.Applications.Common.Models.Image
{
    /// <summary>
    /// 外部图片文件
    /// </summary>
    public class ExternalImageInfo : ImageInfo
    {
        /// <summary>
        /// </summary>
        public ExternalImageInfo()
        {
            Type = ImageInfoType.EXTERNAL;
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Url { get; set; }
    }
}
