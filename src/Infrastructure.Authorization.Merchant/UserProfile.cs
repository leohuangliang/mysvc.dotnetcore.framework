using System;
using System.Collections.Generic;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant
{
    public class UserProfile
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 区号
        /// </summary>
        public string DialCode { get; set; }

        /// <summary>
        /// 手机号是否绑定
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 邮箱是否确认
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 是否设置了支付密码
        /// </summary>
        public bool HasPaymentPassword { get; set; }
    }
}
