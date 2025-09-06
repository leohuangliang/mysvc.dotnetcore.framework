using System;
using MySvc.Framework.Domain.Core.Impl;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;

namespace MySvc.Framework.Domain.Core.Models
{
    /// <summary>
    /// 表示金钱类型
    /// </summary>
    public class Money : ValueObject<Money>
    {
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
        /// 币种
        /// </summary>
        public Currency Currency { get; private set; }

        /// <summary>
        /// 金额数字
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Money Clone()
        {
            return new Money(this.Currency, this.Amount, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.Amount} {this.Currency.GetName()}";
        }

        /// <summary>
        /// 加法运算符重载
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        /// <returns>运算结果</returns>
        /// <exception cref="ArgumentNullException">当操作数为null时抛出</exception>
        /// <exception cref="ArgumentException">当币种不同时抛出</exception>
        public static Money operator +(Money? left, Money? right)
        {
            // 如果全为null，抛出异常
            if (left is null && right is null)
                throw new ArgumentException("操作数不能全为null");

            // 如果其中一个为null，将null视为0，币种使用不为null的币种
            if (left is null)
                return new Money(right!.Currency, right.Amount, null);
            if (right is null)
                return new Money(left.Currency, left.Amount, null);

            // 两个都不为null时，检查币种是否相同
            if (left.Currency != right.Currency)
                throw new ArgumentException("币种不同，无法进行运算");

            return new Money(left.Currency, left.Amount + right.Amount, null);
        }

        /// <summary>
        /// 减法运算符重载
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        /// <returns>运算结果</returns>
        /// <exception cref="ArgumentNullException">当操作数为null时抛出</exception>
        /// <exception cref="ArgumentException">当币种不同时抛出</exception>
        public static Money operator -(Money? left, Money? right)
        {
            // 如果全为null，抛出异常
            if (left is null && right is null)
                throw new ArgumentException("操作数不能全为null");

            // 如果其中一个为null，将null视为0，币种使用不为null的币种
            if (left is null)
                return new Money(right!.Currency, -right.Amount, null);
            if (right is null)
                return new Money(left.Currency, left.Amount, null);

            // 两个都不为null时，检查币种是否相同
            if (left.Currency != right.Currency)
                throw new ArgumentException("币种不同，无法进行运算");

            return new Money(left.Currency, left.Amount - right.Amount, null);
        }
    }
}