using System;
using Capmarvel.Framework.Domain.Common.Constants;
using Capmarvel.Framework.Domain.Common.Models;

namespace Capmarvel.Framework.Domain.Common.Helpers
{
    /// <summary>
    /// 体积工具类
    /// </summary>
    public class VolumeHelper
    {
        /// <summary>
        /// 每立方米（CBM）等于1000000立方厘米（CBCM）
        /// </summary>
        private const decimal CBCM_PER_CBM = 1000000;

        /// <summary>
        /// 转换体积单位
        /// </summary>
        /// <param name="fromUnit">原始单位</param>
        /// <param name="toUnit">目标单位</param>
        /// <param name="value">需要转换的体积值</param>
        /// <returns>目标体积值</returns>
        public static decimal ConvertVolume(string fromUnit, string toUnit, decimal value)
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
                throw new ArgumentException(string.Format("Can not convert volume negative value {0}", value));
            }

            if (fromUnit == VolumeUnit.CBCM)
            {
                //从G转换为目标单位
                switch (toUnit)
                {
                    case VolumeUnit.CBCM:
                        return value;

                    case VolumeUnit.CBM:
                        return value / CBCM_PER_CBM; 
                    default:
                        throw new ArgumentException(string.Format("Can not convert {0} to {1} with volume value {2}",
                            fromUnit, toUnit, value));
                }
            }

            //如果目标是CBCM
            if (toUnit == VolumeUnit.CBCM)
            {
                switch (fromUnit)
                {
                    case VolumeUnit.CBCM:
                        return value;

                    case VolumeUnit.CBM:
                        return value * CBCM_PER_CBM;
                    default:
                        throw new ArgumentException(
                            string.Format("Can not convert {0} to {1} with volume value {2}", fromUnit, toUnit, value));
                }
            }

            return ConvertVolume(VolumeUnit.CBCM, toUnit, ConvertVolume(fromUnit, VolumeUnit.CBCM, value));
        }

        /// <summary>
        /// 根据尺寸信息计算体积
        /// </summary>
        /// <param name="packing">尺寸</param>
        /// <param name="volumeUnit">体积单位，默认为立方米</param>
        /// <returns>体积</returns>
        public static Volume CalcVolume(Packing packing, string volumeUnit = VolumeUnit.CBM)
        {
            //现将尺寸长度转换为厘米单位
            var cmPacking = packing.ConvertLengthUnit(LengthUnit.CM);

            //计算体积(立方厘米)
            var volumeValue = cmPacking.Length * cmPacking.Width * cmPacking.Height;
            volumeValue = DecimalHelper.RoundUp(volumeValue, 3);

            return new Volume(ConvertVolume(VolumeUnit.CBCM, volumeUnit, volumeValue), volumeUnit);
        }
    }
}
