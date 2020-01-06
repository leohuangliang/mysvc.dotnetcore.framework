using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models
{
    /// <summary>
    /// 表示金钱类型
    /// </summary>
    public class Money : ValueObject<Money>
    {
        private Money()
        {
            
        }

        public Money(decimal amount, string currencyCode)
        {
            Amount = amount;
            CurrencyCode = currencyCode != null ? currencyCode.ToUpper().Trim() : currencyCode;
        }

        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// 货币类型
        /// </summary>
        public string CurrencyCode { get; private set; }
    }
}