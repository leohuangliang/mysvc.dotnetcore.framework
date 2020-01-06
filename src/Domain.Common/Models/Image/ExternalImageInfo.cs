using System;
using System.Collections.Generic;
using System.Text;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Models.Image
{
    /// <summary>
    /// 外部图片文件
    /// </summary>
    public class ExternalImageInfo : ImageInfo
    {
        public ExternalImageInfo(string url, int sort) : base(ImageInfoType.EXTERNAL, sort)
        {
            this.Url = url;
        }
        
        /// <summary>
        /// 图片地址
        /// </summary>
        public string Url { get; set; }
    }
}
