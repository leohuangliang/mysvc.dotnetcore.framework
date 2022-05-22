using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.InternalClient
{
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> RequiredPermissions { get; }
        public bool IsOr { get; } = false;

        public PermissionsAuthorizationRequirement(IEnumerable<string> requiredPermissions)
        {
            this.RequiredPermissions = requiredPermissions;
        }

        public PermissionsAuthorizationRequirement(IEnumerable<string> requiredPermissions,bool isOr = false)
        {
            this.RequiredPermissions = requiredPermissions;
            this.IsOr = isOr;
        }
    }
}
