using Capmarvel.Framework.Domain.Core.Impl;

namespace Capmarvel.Framework.Domain.Common.Models
{
    /// <summary>
    /// 账户信息
    /// </summary>
    public class AccountInfo : ValueObject<AccountInfo>
    {
        public AccountInfo(string tenantCode, string userName, string userFullName = "")
        {
            TenantCode = tenantCode;
            UserName = userName;
            UserFullName = userFullName;
        }

        /// <summary>
        /// 租户代码
        /// </summary>
        public string TenantCode { get; private set; }

        /// <summary>
        /// 用户名（同一租户下用户唯一标示）
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserFullName { get; private set; }
    }
}
