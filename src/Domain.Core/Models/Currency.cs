using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MySvc.DotNetCore.Framework.Domain.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum Currency
    {
        /// <summary>
        /// 人民币（离岸）
        /// </summary>
        [Description("人民币（离岸）")]
        CNH,
        /// <summary>
        /// 美元
        /// </summary>
        [Description("美元")]
        USD,
        /// <summary>
        /// 欧元
        /// </summary>
        [Description("欧元")]
        EUR,
        /// <summary>
        /// 日元
        /// </summary>
        [Description("日元")]
        JPY,
        /// <summary>
        /// 港币
        /// </summary>
        [Description("港币")]
        HKD,
        /// <summary>
        /// 英镑
        /// </summary>
        [Description("英镑")]
        GBP,

        /// <summary>
        /// 人民币（在岸）
        /// </summary>
        [Description("人民币（在岸）")]
        CNY,

        /// <summary>
        /// 澳元
        /// </summary>
        [Description("澳元")]
        AUD,

        /// <summary>
        /// 加拿大元
        /// </summary>
        [Description("加拿大元")]
        CAD,

        /// <summary>
        /// 丹麦克朗
        /// </summary>
        [Description("丹麦克朗")]
        DKK,

        /// <summary>
        /// 瑞士法郎
        /// </summary>
        [Description("瑞士法郎")]
        CHF,

        /// <summary>
        /// 挪威克朗
        /// </summary>
        [Description("挪威克朗")]
        NOK,

        /// <summary>
        /// 新西兰元
        /// </summary>
        [Description("新西兰元")]
        NZD,

        /// <summary>
        /// 瑞典克朗
        /// </summary>
        [Description("瑞典克朗")]
        SEK,

        /// <summary>
        /// 新加坡元
        /// </summary>
        [Description("新加坡元")]
        SGD,
    }

    /// <summary>
    /// 
    /// </summary>
    public static class CurrencyHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyString"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static Currency GetCurrency(string currencyString)
        {

            bool ok = Enum.TryParse(currencyString, out Currency currency);
            if (ok) return currency;
            else
            {
                throw new InvalidCastException("Convert to Currency error");
            }
        }
    }
}
