using System;
using System.Collections.Generic;
using System.Security.Authentication;
using MySvc.DotNetCore.Framework.Infrastructure.Authorization.Admin.Permissions;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

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
            //List<string> permissions = PermissionManage.GetPermissions(user.Role);
            List<string> permissions = new List<string>();
            return new UserIdentity(user.UserId, user.UserName, user.FullName, user.Email, user.ConfirmEmail, user.DialCode, user.PhoneNumber, user.ConfirmPhoneNumber, user.Role, permissions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private UserIdentity MapUser()
        {
            string userId = _contextAccessor.HttpContext.User.GetClaimValue("sub");
            string userName = _contextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Name);
            string role = _contextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Role);
            string fullName = _contextAccessor.HttpContext.User.GetClaimValue("full_name");
            string email = _contextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Email);
            string dialcode = _contextAccessor.HttpContext.User.GetClaimValue("dialcode");
            string phone_number = _contextAccessor.HttpContext.User.GetClaimValue("phone_number");
            string email_verified = _contextAccessor.HttpContext.User.GetClaimValue("email_verified");
            string phone_number_verified = _contextAccessor.HttpContext.User.GetClaimValue("phone_number_verified");

            bool bool_email_verified = false;
            bool.TryParse(email_verified, out bool_email_verified);

            bool bool_phone_number_verified = false;
            bool.TryParse(phone_number_verified, out bool_phone_number_verified);
            return new UserIdentity(userId, userName, fullName, email, bool_email_verified, dialcode, phone_number, bool_phone_number_verified, role, new List<string>());
        }
    }
}