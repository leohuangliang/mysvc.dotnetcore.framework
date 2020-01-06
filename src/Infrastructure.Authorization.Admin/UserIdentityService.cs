using System;
using System.Collections.Generic;
using System.Security.Authentication;
using MySvc.DotNetCore.Framework.Infrastructure.Authorization.Admin.Permissions;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Admin
{
    public class UserIdentityService : IUserIdentityService
    {
        private IHttpContextAccessor _contextAccessor;
        private readonly IJsonConverter _jsonConverter;

        private readonly ILogger<UserIdentityService> _logger;
        public UserIdentityService(IHttpContextAccessor contextAccessor, IJsonConverter jsonConverter,
            ILogger<UserIdentityService> logger)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _jsonConverter = jsonConverter ?? throw new ArgumentNullException(nameof(jsonConverter));
            _logger = logger;
        }
        public UserIdentity GetUserIdentity()
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthenticationException("unauthorized");
            }

            var user = MapUser();
            List<string> permissions = PermissionManage.GetPermissions(user.Role);
            return new UserIdentity(user.UserId, user.UserName, user.FullName, user.Email, user.PhoneNumber, user.Role, permissions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private UserIdentity MapUser()
        {
            string userId = _contextAccessor.HttpContext.User.FindFirst("sub").Value;
            string userName = _contextAccessor.HttpContext.User.FindFirst("unique_name").Value;

            var roleClaim = _contextAccessor.HttpContext.User.FindFirst("role");
            string role = "";
            if (roleClaim != null)
            {
                role = _contextAccessor.HttpContext.User.FindFirst("role").Value;
            }

            var fullNameClaim = _contextAccessor.HttpContext.User.FindFirst("full_name");
            string fullName = "";
            if (fullNameClaim != null)
            {
                fullName = _contextAccessor.HttpContext.User.FindFirst("full_name").Value;
            }

            var email = _contextAccessor.HttpContext.User.FindFirst("email").Value;
            var phone_number = _contextAccessor.HttpContext.User.FindFirst("phone_number").Value;

            if (userId.IsNullOrBlank()  || userName.IsNullOrBlank())
            {
                throw new AuthenticationException("unauthorized");
            }
            return new UserIdentity(userId, userName, fullName, email, phone_number, role, new List<string>());
        }
    }
}