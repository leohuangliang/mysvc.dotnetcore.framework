using MySvc.Framework.Domain.Core.Impl;

namespace MySvc.Framework.Domain.Core.Models
{
    /// <summary>
    /// 操作员
    /// </summary>
    public class Operator :ValueObject<Operator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName">用户姓名</param>
        /// <param name="fullName">全名</param>
        /// <param name="phone">手机号</param>
        /// <param name="email">邮箱</param>
        /// <param name="operatorType"></param>
        public Operator(string userName,string phone, string email, OperatorType operatorType)
        {
            this.UserName = userName;
            this.Phone = phone;
            this.Email = email;
            this.OperatorType = operatorType;
        }

        /// <summary>
        /// 操作人用户姓名
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; private set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// 操作人类型
        /// </summary>
        public OperatorType OperatorType { get; private set; }

        /// <summary>
        /// 创建系统操作员
        /// </summary>
        /// <returns></returns>
        public static Operator CreateSystemOperator()
        {
            return new Operator(
                userName: "System",
                phone: "",
                email:"",
                OperatorType.System);
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
