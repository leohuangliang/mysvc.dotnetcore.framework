namespace Capmarvel.Framework.Applications.Common.Models
{
    /// <summary>
    /// 体积 信息
    /// </summary>
    public class Volume
    {
        /// <summary>
        /// 体积值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 带单位显示
        /// </summary>
        /// <returns></returns>
        public string Display()
        {
            return Value + Unit;
        }
    }
}