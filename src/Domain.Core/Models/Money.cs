﻿using System;
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
            return new Money(this.Currency, this.Amount);
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
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Money operator +(Money left, Money right)
        {
            Money amountInfo = null;
            if (left.Currency == right.Currency)
            {
                amountInfo = new Money(left.Currency, left.Amount + right.Amount, null);
            }
            else
            {
                throw new ArgumentException("不支持不同币种之间加法运算： ", nameof(left));
            }

            return amountInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Money operator -(Money left, Money right)
        {
            Money amountInfo = null;
            if (left.Currency == right.Currency)
            {
                amountInfo = new Money(left.Currency, left.Amount - right.Amount, null);
            }
            else
            {
                throw new ArgumentException("不支持不同币种之间减法运算： ", nameof(left));
            }

            return amountInfo;
        }
    }
}