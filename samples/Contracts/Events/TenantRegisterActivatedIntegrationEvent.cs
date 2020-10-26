using System;

namespace Contracts.Events
{
    public class TenantRegisterActivatedIntegrationEvent
    {
        
        /// <summary>
        /// 租户代码
        /// </summary>
        public  string TenantCode { get; set; }

        /// <summary>
        /// 租户账号的拥有者，存租户用户名
        /// </summary>
        public string TenantOwnerUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 激活时间
        /// </summary>
        public DateTime ActivationTime { get; set; }

    }
}
