using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Order.Application.Commands
{
    public class TenantRegisterActivatedCommand : IRequest<bool>
    {
        public TenantRegisterActivatedCommand()
        {
        }
        /// <summary>
        /// 租户代码
        /// </summary>
        public string TenantCode { get;  set; }

        /// <summary>
        /// 租户账号的拥有者，存租户用户名
        /// </summary>
        public string TenantOwnerUserName { get;  set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get;  set; }

        /// <summary>
        /// 激活时间
        /// </summary>
        public DateTime ActivationTime { get;  set; }
    }
}
