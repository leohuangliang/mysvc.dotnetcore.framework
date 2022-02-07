using Microsoft.AspNetCore.Mvc;

namespace MySvc.Framework.Infrastructure.Authorization.Merchant
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 权限且关系
        /// </summary>
        /// <param name="permissions"></param>
        public PermissionAttribute(params string[] permissions)
            : base(typeof(PermissionAttributeFilter))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(permissions) };
        }

        /// <summary>
        /// 权限且/或关系
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="isOr">true=或关系;false=且关系</param>
        public PermissionAttribute(bool isOr, params string[] permissions)
           : base(typeof(PermissionAttributeFilter))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(permissions, isOr) };
        }
    }
}
