using System;
using Capmarvel.Framework.Domain.Common.Constants;
using Capmarvel.Framework.Domain.Common.Models;

namespace Capmarvel.Framework.Domain.Common.Helpers
{
    /// <summary>
    /// 重量换算工具类
    /// </summary>
    public static class WeightHelper
    {
        /// <summary>
        /// 每1盎司等于 28.349523125克
        /// </summary>
        private const decimal G_PER_OZ = 28.349523125M;

        /// <summary>
        /// 每1磅等于 453.59237克
        /// </summary>
        private const decimal G_PER_LBS = 453.59237M;

        /// <summary>
        /// 每1千克等于 1000克
        /// </summary>
        private const decimal G_PER_KG = 1000M;

        /// <summary>
        /// 空重量（单位为G）
        /// </summary>
        public static Weight NullWeightInG
        {
            get { return new Weight(0, WeightUnit.G); }
        }

        /// <summary>
        /// 克（G）转盎司（OZ）
        /// </summary>
        /// <param name="gram">重量值（克）</param>
        /// <returns>转换后的重量值（盎司）</returns>
        public static int Gram2Ounce(decimal gram)
        {
            return Convert.ToInt32(ConvertWeight(WeightUnit.G, WeightUnit.OZ, gram));
        }

        /// <summary>
        /// 盎司（OZ）转克（G）
        /// </summary>
        /// <param name="ounce">重量值（盎司）</param>
        /// <returns>转换后的重量值（克）</returns>
        public static int Ounce2Gram(decimal ounce)
        {
            return Convert.ToInt32(ConvertWeight(WeightUnit.OZ, WeightUnit.G, ounce));
        }

        /// <summary>
        /// 克（G）转磅（LBS）
        /// </summary>
        /// <param name="gram">重量值（克）</param>
        /// <returns>转换后的重量值（磅）</returns>
        public static int Gram2Pound(decimal gram)
        {
            return Convert.ToInt32(ConvertWeight(WeightUnit.G, WeightUnit.LBS, gram));
        }

        /// <summary>
        /// 磅（LBS）转 克（G）
        /// </summary>
        /// <param name="pound">重量值（磅）</param>
        /// <returns>转换后的重量值（克）</returns>
        public static int Pound2Gram(decimal pound)
        {
            return Convert.ToInt32(ConvertWeight(WeightUnit.LBS, WeightUnit.G, pound));
        }

        /// <summary>
        ///  克（G）转千克（KG）
        /// </summary>
        /// <param name="gram">重量值（克）</param>
        /// <returns>转换后的重量值（千克）</returns>
        public static decimal Gram2Kg(decimal gram)
        {
            return ConvertWeight(WeightUnit.G, WeightUnit.KG, gram);
        }

        /// <summary>
        /// 千克（KG）转 克（G）
        /// </summary>
        /// <param name="kg">重量值（千克）</param>
        /// <returns>转换后的重量值（克）</returns>
        public static int Kg2Gram(decimal kg)
        {
            return Convert.ToInt32(ConvertWeight(WeightUnit.KG, WeightUnit.G, kg));
        }

        /// <summary>
        /// 转换重量单位
        /// </summary>
        /// <param name="fromUnit">原始单位</param>
        /// <param name="toUnit">目标单位</param>
        /// <param name="value">需要转换的重量值</param>
        /// <returns>目标重量值</returns>
        public static decimal ConvertWeight(string fromUnit, string toUnit, decimal value)
        {
            if (value == 0)
            {
                return 0;
            }

            if (fromUnit == toUnit)
            {
                return value;
            }

            //如果是负数
            if (value < 0)
            {
                throw new ArgumentException(string.Format("Can not convert negative value {0}", value));
            }

            if (fromUnit == WeightUnit.G)
            {
                //从G转换为目标单位
                switch (toUnit)
                {
                    case WeightUnit.G:
                        return value;

                    case WeightUnit.KG:
                        return value / G_PER_KG;

                    case WeightUnit.OZ:
                        return value / G_PER_OZ;

                    case WeightUnit.LBS:
                        return value / G_PER_LBS;
                    default:
                        throw new ArgumentException(string.Format("Can not convert {0} to {1} with value {2}", 
                            fromUnit, toUnit, value));
                }
            }

            //如果目标是G
            if (toUnit == WeightUnit.G)
            {
                switch (fromUnit)
                {
                    case WeightUnit.G:
                        return value;

                    case WeightUnit.KG:
                        return value * G_PER_KG;

                    case WeightUnit.OZ:
                        return value * G_PER_OZ;

                    case WeightUnit.LBS:
                        return value * G_PER_LBS;

                    default:
                        throw new ArgumentException(
                            string.Format("Can not convert {0} to {1} with value {2}", fromUnit, toUnit, value));
                }
            }

            return ConvertWeight(WeightUnit.G, toUnit, ConvertWeight(fromUnit, WeightUnit.G, value));
        }

        /// <summary>
        /// 重量值向上取整
        /// </summary>
        /// <param name="weight">重量</param>
        /// <param name="precision">精度</param>
        /// <returns>调整后的重量</returns>
        public static Weight RoundUp(Weight weight, int? precision = null)
        {
            switch (weight.Unit)
            {
                case WeightUnit.G:
                    return new Weight(DecimalHelper.RoundUp(weight.Value, precision ?? 0), weight.Unit);
                case WeightUnit.KG:
                    return new Weight(DecimalHelper.RoundUp(weight.Value, precision ?? 3), weight.Unit);
                case WeightUnit.LBS:
                    return new Weight(DecimalHelper.RoundUp(weight.Value, precision ?? 7), weight.Unit);
                case WeightUnit.OZ:
                    return new Weight(DecimalHelper.RoundUp(weight.Value, precision ?? 6), weight.Unit);
            }

            return weight;
        }
    }
}
