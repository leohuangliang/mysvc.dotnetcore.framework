using System.Linq;
using MySvc.Framework.Infrastructure.Authorization.Admin.Permissions;
using MySvc.Framework.Infrastructure.Crosscutting.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace MySvc.Framework.Infrastructure.Authorization.Admin
{
    public class PermissionAttributeFilter : IAuthorizationFilter
    {

        private readonly PermissionsAuthorizationRequirement _requiredPermissions;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IPermissionProvider _permissionProvider;

        public PermissionAttributeFilter(PermissionsAuthorizationRequirement requiredPermissions, IUserIdentityService userIdentityService, IPermissionProvider permissionProvider)
        {
            _requiredPermissions = requiredPermissions;
            _userIdentityService = userIdentityService;
            _permissionProvider = permissionProvider ?? throw new ArgumentNullException(nameof(permissionProvider));
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasPermission = false;

            var user = _userIdentityService.GetUserIdentity();

            var permissions =  _permissionProvider.GetPermissionsAsync(user.UserId).GetAwaiter().GetResult();


            if (_requiredPermissions.IsOr)
            {
                //包含任何一个权限码，表示有权限

                hasPermission = _requiredPermissions.RequiredPermissions.Any(c =>
                    permissions.Contains(c));

            }
            else
            {
                //必须包含权限码，才表示有权限
                hasPermission =
                    _requiredPermissions.RequiredPermissions.All(c =>
                        permissions.Contains(c));

            }

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
