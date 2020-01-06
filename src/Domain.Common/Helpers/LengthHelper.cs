using System;
using Capmarvel.Framework.Domain.Common.Constants;

namespace Capmarvel.Framework.Domain.Common.Helpers
{
    /// <summary>
    /// 长度换算工具类
    /// </summary>
    public class LengthHelper
    {
        /// <summary>
        /// 每米（M）等于100厘米（CM）
        /// </summary>
        private const decimal CENTI_PER_METRE = 100;

        /// <summary>
        /// 每英寸（IN）等于2.54厘米（CM）
        /// </summary>
        private const decimal CENTI_PER_INCH = 2.54M;

        /// <summary>
        /// 每英尺（FT）等于30.48厘米（CM）
        /// </summary>
        private const decimal CENTI_PER_FEET = 30.48M;

        /// <summary>
        /// 厘米（M）转米（CM）
        /// </summary>
        /// <param name="centi">长度值（厘米）</param>
        /// <returns>转换后的长度值（米）</returns>
        public static int Centi2Metre(decimal centi)
        {
            return (int)ConvertLength(LengthUnit.CM, LengthUnit.M, centi);
        }

        /// <summary>
        /// 米（M）转厘米（CM）
        /// </summary>
        /// <param name="metre">长度值（米）</param>
        /// <returns>转换后的长度值（厘米）</returns>
        public static decimal Metre2Centi(decimal metre)
        {
            return (int)ConvertLength(LengthUnit.M, LengthUnit.CM, metre);
        }

        /// <summary>
        /// 转换长度单位
        /// </summary>
        /// <param name="fromUnit">原始单位</param>
        /// <param name="toUnit">目标单位</param>
        /// <param name="value">需要转换的长度值</param>
        /// <returns>目标长度值</returns>
        public static decimal ConvertLength(string fromUnit, string toUnit, decimal value)
        {
            if (value == 0)
            {
                return 0;
            }

            if (value < 0)
            {
                throw new ArgumentException(string.Format("Can not convert negative value {0}", value));
            }

            if (fromUnit == LengthUnit.CM)
            {
                switch (toUnit)
                {
                    case LengthUnit.CM:
                        return value;

                    case LengthUnit.M:
                        return value / CENTI_PER_METRE;

                    case LengthUnit.IN:
                        return value / CENTI_PER_INCH;

                    case LengthUnit.FT:
                        return value / CENTI_PER_FEET;

                    default:
                        throw new ArgumentException(string.Format("Can not convert {0} to {1} with value {2}", fromUnit, toUnit, value));
                }
            }

            if (toUnit == LengthUnit.CM)
            {
                switch (fromUnit)
                {
                    case LengthUnit.CM:
                        return value;

                    case LengthUnit.M:
                        return value * CENTI_PER_METRE;

                    case LengthUnit.IN:
                        return value * CENTI_PER_INCH;

                    case LengthUnit.FT:
                        return value * CENTI_PER_FEET;

                    default:
                        throw new ArgumentException(string.Format("Can not convert {0} to {1} with value {2}", fromUnit, toUnit, value));
                }
            }

            return ConvertLength(LengthUnit.CM, toUnit, ConvertLength(fromUnit, LengthUnit.CM, value));
        }
    }
}
