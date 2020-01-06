namespace Capmarvel.Framework.Applications.Common.Models
{
    /// <summary>
    /// 重量信息
    /// </summary>
    public class Weight
    {
        /// <summary>
        /// 重量值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 重量单位
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