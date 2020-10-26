﻿using System;
using MySvc.DotNetCore.Framework.Domain.Core.Helpers;
using MySvc.DotNetCore.Framework.Domain.Core.Impl;

namespace MySvc.DotNetCore.Framework.Domain.Core.Models
{
    /// <summary>
    /// 表示金钱类型
    /// </summary>
    public class Money : ValueObject<Money>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        public Money(Currency currency, decimal amount)
        {
            Amount = amount;
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
                amountInfo = new Money(left.Currency, left.Amount + right.Amount);
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
                amountInfo = new Money(left.Currency, left.Amount - right.Amount);
            }
            else
            {
                throw new ArgumentException("不支持不同币种之间减法运算： ", nameof(left));
            }

            return amountInfo;
        }
    }
}