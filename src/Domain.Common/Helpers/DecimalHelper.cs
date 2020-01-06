using System;

namespace Capmarvel.Framework.Domain.Common.Helpers
{

    /// <summary>
    /// Decimal数值辅助类
    /// </summary>
    public static class DecimalHelper
    {
        /// <summary>
        /// 数值四舍五入（中国式四舍五入）
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="precision">精度（小数点后几位）</param>
        /// <returns>四舍五入后的数值</returns>
        public static Decimal GetRound(Decimal value, int precision)
        {
            return Math.Round(value, precision, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 数值向上取整
        /// 精度为2， 0.342 向上取整 0.35
        /// 精度为4， 0.3423 向上取整 0.3423
        /// 精度为4， 0.34232 向上取整 0.3424
        /// </summary>
        /// <param name="val">数值</param>
        /// <param name="decPoint">精度（小数点后几位）</param>
        /// <returns>向上取整后的数值</returns>
        public static Decimal RoundUp(Decimal val, int decPoint = 1)
        {
            bool flag = false;
            if (val < new Decimal(0))
            {
                val = -val;
                flag = true;
            }

            Decimal num1 = Math.Round(val, decPoint, MidpointRounding.AwayFromZero);
            if (val - num1 > new Decimal(0))
            {
                Decimal num2 = new Decimal(1) / (Decimal)Math.Pow(10.0, (double)decPoint);
                num1 += num2;
            }

            if (flag)
            {
                return num1 * new Decimal(-1);
            }

            return num1;
        }
    }
}
