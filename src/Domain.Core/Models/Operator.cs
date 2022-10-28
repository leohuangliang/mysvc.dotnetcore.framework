using MySvc.Framework.Domain.Core.Impl;

namespace MySvc.Framework.Domain.Core.Models
{
    /// <summary>
    /// 操作员
    /// </summary>
    public class Operator
    {


        /// <summary>
        /// 操作人用户姓名
        /// </summary>
        public string UserName { get; init; }

        public string FullName { get; init; }

        public string DialCode { get; init; }

        public string PhoneNumber { get; init; }

         
        /// <summary>
        /// 操作人类型
        /// </summary>
        public OperatorType OperatorType { get;  init; }

        /// <summary>
        /// 创建系统操作员
        /// </summary>
        /// <returns></returns>
        public static Operator CreateSystemOperator()
        {
            return new Operator()
            {
                UserName = "system",
                FullName = "system",
                OperatorType = OperatorType.System
            };
        }

        
    }

    /// <summary>
    /// 操作人类型
    /// </summary>
    public enum OperatorType
    {
        /// <summary>
        /// 客户
        /// </summary>
        Customer,

        /// <summary>
        /// 内部员工
        /// </summary>
        Employee,

        /// <summary>
        /// 系统
        /// </summary>
        System
    }
}
