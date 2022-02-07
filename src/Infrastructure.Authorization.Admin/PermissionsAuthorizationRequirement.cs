using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MySvc.Framework.Infrastructure.Authorization.Admin
{
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> RequiredPermissions { get; }
        public bool IsOr { get; } = false;

        public PermissionsAuthorizationRequirement(IEnumerable<string> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }

        public PermissionsAuthorizationRequirement(IEnumerable<string> requiredPermissions,bool isOr = false)
        {
            RequiredPermissions = requiredPermissions;
            IsOr = isOr;
        }
    }
}
