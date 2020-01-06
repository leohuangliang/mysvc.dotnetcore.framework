using System;
using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models
{
    /// <summary>
    /// 操作人信息
    /// </summary>
    public class Operator : ValueObject<Operator>
    {
        private Operator()
        {
        }

        public Operator(string code, string name, string operatorType)
        {
            Code = code;
            Name = name;
            OperatorType = operatorType;
        }

        /// <summary>
        /// 操作人Id
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 操作人类型 
        /// </summary>
        public string OperatorType { get; private set; }

        /// <summary>
        /// 克隆当前Operator对象，生成一个新的对象
        /// </summary>
        /// <returns>Operator对象</returns>
        public Operator CloneOperator()
        {
            return new Operator(this.Code, this.Name, this.OperatorType);
        }

        /// <summary>
        /// 业务意义代表空的Operator对象
        /// </summary>
        /// <returns></returns>
        public static Operator NullOperator()
        {
            return new Operator(string.Empty, string.Empty, Constants.OperatorType.SYSTEM);
        }
    }
}
