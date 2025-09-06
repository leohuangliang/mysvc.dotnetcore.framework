using System;
using System.Collections.Generic;

namespace MySvc.Framework.Infrastructure.Authorization.Merchant
{
    public class UserProfile
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// 区号
        /// </summary>
        public string DialCode { get; set; } = string.Empty;

        /// <summary>
        /// 手机号是否绑定
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱是否确认
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// 是否设置了支付密码
        /// </summary>
        public bool HasPaymentPassword { get; set; }

        public string UID { get; set; } = string.Empty;
    }
}
