using System.Linq;
using MySvc.Framework.Infrastructure.Crosscutting.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MySvc.Framework.Infrastructure.Authorization.Merchant
{
    public class PermissionAttributeFilter : IAuthorizationFilter
    {

        private readonly IJsonConverter _jsonConverter;
        private readonly PermissionsAuthorizationRequirement _requiredPermissions;
        private readonly IUserIdentityService _userIdentityService;

        public PermissionAttributeFilter(
            IJsonConverter jsonConverter, 
            PermissionsAuthorizationRequirement requiredPermissions, 
            IUserIdentityService userIdentityService)
        {
            _jsonConverter = jsonConverter ?? throw new System.ArgumentNullException(nameof(jsonConverter));
            _requiredPermissions = requiredPermissions ?? throw new System.ArgumentNullException(nameof(requiredPermissions));
            _userIdentityService = userIdentityService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }
            //bool hasPermission = true;


            //var user = _userIdentityService.GetUserIdentityAsync().Result;

            //if (user.Permissions.Any())
            //{
            //    bool allMatch = true;
            //    foreach (var requiredPermissionsRequiredPermission in _requiredPermissions.RequiredPermissions)
            //    {
            //        if (!_requiredPermissions.IsOr)
            //        {
            //            if (!user.Permissions.Contains(requiredPermissionsRequiredPermission))
            //            {
            //                allMatch = false;
            //                break;
            //            }
            //        }
            //        else
            //        {
            //            if (user.Permissions.Contains(requiredPermissionsRequiredPermission))
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

            //if (!hasPermission)
            //{
            //    context.Result = new ForbidResult();
            //}
        }
    }
}
