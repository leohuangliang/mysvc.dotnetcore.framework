namespace Capmarvel.Framework.Applications.Common.Models
{
    /// <summary>
    /// 账户信息
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// 租户代码
        /// </summary>
        public string TenantCode { get; set; }

        /// <summary>
        /// 用户名（同一租户下用户唯一标示）
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserFullName { get; set; }
    }
}
