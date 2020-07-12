﻿using System;

namespace Contracts.Events
{
    public class TenantRegisterActivatedIntegrationEvent
    {
        public TenantRegisterActivatedIntegrationEvent(string tenantCode, string tenantOwnerUserName, DateTime createTime, DateTime activationTime)
        {

            this.TenantCode = tenantCode;
            this.TenantOwnerUserName = tenantOwnerUserName;
            this.CreateTime = createTime;
            this.ActivationTime = activationTime;
        }
        /// <summary>
        /// 租户代码
        /// </summary>
        public string TenantCode { get; private set; }

        /// <summary>
        /// 租户账号的拥有者，存租户用户名
        /// </summary>
        public string TenantOwnerUserName { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 激活时间
        /// </summary>
        public DateTime ActivationTime { get; private set; }

    }
}