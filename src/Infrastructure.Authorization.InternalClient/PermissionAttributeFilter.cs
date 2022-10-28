using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySvc.Framework.Infrastructure.Crosscutting.Json;

namespace MySvc.Framework.Infrastructure.Authorization.InternalClient
{
    public class PermissionAttributeFilter : IAuthorizationFilter
    {

        private readonly IJsonConverter _jsonConverter;
        private readonly PermissionsAuthorizationRequirement _requiredPermissions;
        private readonly IUserIdentityService _userIdentityService;

        public PermissionAttributeFilter(IJsonConverter jsonConverter, PermissionsAuthorizationRequirement requiredPermissions, IUserIdentityService userIdentityService)
        {
            _jsonConverter = jsonConverter;
            _requiredPermissions = requiredPermissions;
            _userIdentityService = userIdentityService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }
            bool hasPermission = true;

            //string role = context.HttpContext.User.FindFirst("role").Value;

            //var permissions = PermissionManage.GetPermissions(role);

            //if (permissions.Any())
            //{
            //    bool allMatch = true;
            //    foreach (var requiredPermissionsRequiredPermission in _requiredPermissions.RequiredPermissions)
            //    {
            //        if (!_requiredPermissions.IsOr)
            //        {
            //            if (!permissions.Contains(requiredPermissionsRequiredPermission))
            //            {
            //                allMatch = false;
            //                break;
            //            }
            //        }
            //        else
            //        {
            //            if (permissions.Contains(requiredPermissionsRequiredPermission))
            //            {
            //                allMatch = true;
            //                break;
            //            }
            //        }

            //    }

            //    if (allMatch)
            //    {
            //        hasPermission = true;
            //    }
            //}

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
