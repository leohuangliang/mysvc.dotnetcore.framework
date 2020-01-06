namespace Capmarvel.Framework.Applications.Common.Models
{
    /// <summary>
    /// 表示金钱类型
    /// </summary>
    public class Money
    {
        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 货币类型
        /// </summary>
        public string CurrencyCode { get; set; }
    }
}