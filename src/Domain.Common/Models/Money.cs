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

        /// <summary>
        /// 构建金钱类型
        /// </summary>
        /// <param name="currency">货币</param>
        /// <param name="amount">金额</param>
        /// <param name="keepDecimals">保留的小数点的位数，默认2位，为null则不做额外处理</param>
        public Money(Currency currency, decimal amount, int? keepDecimals = 2)
        {
            Amount = keepDecimals != null ? decimal.Round(amount, keepDecimals.Value) : amount;
            Currency = currency;
        }

        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// 货币类型
        /// </summary>
        public string CurrencyCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Money Clone()
        {
            return new Money(this.Currency, this.Amount);
        }
    }
}