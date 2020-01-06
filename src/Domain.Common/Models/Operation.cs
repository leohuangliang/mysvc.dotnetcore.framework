using System;
using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models
{
    /// <summary>
    /// 操作信息
    /// </summary>
    public class Operation : ValueObject<Operation>
    {
        private Operation()
        {
        }

        public Operation(Operator operatorInfo, DateTime time)
        {
            if (time == DateTime.MinValue)
            {
                throw new ArgumentOutOfRangeException("time", "Operation time couldn't be DateTime.MinValue");
            }

            if (time == DateTime.MaxValue)
            {
                throw new ArgumentOutOfRangeException("time", "Operation time couldn't be DateTime.MaxValue");
            }

            Operator = operatorInfo;
            Time = time.ToUniversalTime();
        }

        /// <summary>
        /// 操作者
        /// </summary>
        public Operator Operator { get; private set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Time { get; private set; }

        /// <summary>
        /// 克隆当前Operation对象，生成一个新的对象
        /// </summary>
        /// <returns>Operation对象</returns>
        public Operation CloneOperation()
        {
            return new Operation(this.Operator.CloneOperator(), Time);
        }

        /// <summary>
        /// 业务意义代表空的Operation对象
        /// </summary>
        /// <returns></returns>
        public static Operation NullOperation()
        {
            return new Operation(Operator.NullOperator(), DateTime.MinValue);
        }
    }
}
