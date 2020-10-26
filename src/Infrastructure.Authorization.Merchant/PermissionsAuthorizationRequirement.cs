using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> RequiredPermissions { get; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOr { get; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requiredPermissions"></param>
        public PermissionsAuthorizationRequirement(IEnumerable<string> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requiredPermissions"></param>
        /// <param name="isOr"></param>
        public PermissionsAuthorizationRequirement(IEnumerable<string> requiredPermissions, bool isOr = false)
        {
            RequiredPermissions = requiredPermissions;
            IsOr = isOr;
        }
    }
}
