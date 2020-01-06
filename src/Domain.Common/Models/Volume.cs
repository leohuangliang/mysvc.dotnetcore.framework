using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models
{
    /// <summary>
    /// 体积 信息
    /// </summary>
    public class Volume : ValueObject<Volume>
    {
        private Volume()
        {
        }

        public Volume(decimal value, string unit)
        {
            Value = value;
            Unit = unit != null ? unit.ToUpper() : "";
        }

        /// <summary>
        /// 体积值
        /// </summary>
        public decimal Value { get; private set; }

        /// <summary>
        /// 体积单位
        /// </summary>
        public string Unit { get; private set; }
    }
}
