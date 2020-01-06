using System;
using System.Collections.Generic;
using System.Text;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers
{
    public static class DecimalExtension
    {
        /// <summary>
        /// 无条件保留小数点2位
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal TruncateDecimal2(this decimal value)
        {
            
            decimal result = Math.Truncate(value * 100) / 100;

            return result;
        }
    }
}
