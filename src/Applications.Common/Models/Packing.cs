namespace Capmarvel.Framework.Applications.Common.Models
{

    /// <summary>
    /// 尺寸信息
    /// </summary>
    public class Packing
    {
        /// <summary>
        /// 长度
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// 尺寸单位
        /// </summary>
        public string LengthUnit { get; set; }
    }
}