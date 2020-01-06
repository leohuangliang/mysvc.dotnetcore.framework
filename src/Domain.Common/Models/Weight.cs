using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models
{
    /// <summary>
    /// 重量信息
    /// </summary>
    public class Weight : ValueObject<Weight>
    {
        private Weight()
        {
        }

        public Weight(decimal value, string unit)
        {
            Value = value;
            Unit = unit != null ? unit.ToUpper().Trim() : string.Empty;
        }
        /// <summary>
        /// 重量值
        /// </summary>
        public decimal Value { get; private set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        public string Unit { get; private set; }

        /// <summary>
        /// 复制一个重量Weight
        /// </summary>
        /// <returns>重量Weight</returns>
        public Weight CloneWeight()
        {
            return new Weight(Value, Unit);
        }
    }
}