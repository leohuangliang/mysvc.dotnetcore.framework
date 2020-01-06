using Capmarvel.Framework.Domain.Common.Helpers;
using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models
{
    /// <summary>
    /// 尺寸信息
    /// </summary>
    public class Packing : ValueObject<Packing>
    {
        private Packing()
        {
        }

        public Packing(decimal length, decimal width, decimal height, string lengthUnit)
        {
            Length = length;
            Width = width;
            Height = height;
            LengthUnit = lengthUnit != null ? lengthUnit.ToUpper().Trim() : string.Empty;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public decimal Length { get; private set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Width { get; private set; }

        /// <summary>
        /// 高度
        /// </summary>
        public decimal Height { get; private set; }

        /// <summary>
        /// 尺寸单位
        /// </summary>
        public string LengthUnit { get; private set; }

        /// <summary>
        /// 根据当前尺寸获取对应的体积（立方米）
        /// </summary>
        /// <returns>体积信息</returns>
        public Volume GetVolumeInCBM()
        {
            return VolumeHelper.CalcVolume(this);
        }
       
        /// <summary>
        /// 转换长度单位
        /// </summary>
        /// <param name="lengthUnit">长度单位</param>
        /// <returns>转换长度后</returns>
        public Packing ConvertLengthUnit(string lengthUnit)
        {
            if (LengthUnit != lengthUnit)
            {
                var length = LengthHelper.ConvertLength(LengthUnit, lengthUnit, Length);
                var width = LengthHelper.ConvertLength(LengthUnit, lengthUnit, Width);
                var height = LengthHelper.ConvertLength(LengthUnit, lengthUnit, Height);

                return new Packing(length, width, height, lengthUnit);
            }

            return this;
        }

        /// <summary>
        /// 复制一个尺寸Packing
        /// </summary>
        /// <returns>尺寸Packing</returns>
        public Packing ClonePacking()
        {
            return new Packing(Length, Width, Height, LengthUnit);
        }
    }
}