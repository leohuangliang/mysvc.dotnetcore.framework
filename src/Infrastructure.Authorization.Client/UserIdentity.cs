using System.Collections.Generic;
using System.Linq;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client
{
    public class UserIdentity
    {
        public UserIdentity(string tenantUserId, string tenantCode, string userName, string fullName,
            string email,
            bool confirmEmail,
            string dialCode,
            string phoneNumber,
            bool confirmPhoneNumber,
            bool hasPaymentPasswrd,
            string role, List<string> permissions)
        {
            this.TenantUserId = tenantUserId;
            this.TenantCode = tenantCode;
            this.UserName = userName;
            this.FullName = fullName;
            this.Email = email;
            this.ConfirmEmail = confirmEmail;
            this.DialCode = dialCode;
            this.PhoneNumber = phoneNumber;
            this.ConfirmPhoneNumber = confirmPhoneNumber;
            this.HasPaymentPassword = hasPaymentPasswrd;
            this.Role = role;
            this.Permissions = permissions != null ? permissions : new List<string>();
        }

        public UserIdentity(string tenantUserId, string tenantCode, UserProfile userProfile)
        {
            this.TenantUserId = tenantUserId;
            this.TenantCode = tenantCode;
            this.UserName = userProfile.UserName;
            this.FullName = userProfile.FullName;
            this.Email = userProfile.Email;
            this.ConfirmEmail = userProfile.EmailConfirmed;
            this.DialCode = userProfile.DialCode;
            this.PhoneNumber = userProfile.PhoneNumber;
            this.ConfirmPhoneNumber = userProfile.PhoneNumberConfirmed;
            this.HasPaymentPassword = userProfile.HasPaymentPassword;
            this.Role = userProfile.Role;
            this.Permissions = userProfile.Permissions != null ? userProfile.Permissions : new List<string>();

        }

        public string TenantUserId { get; private set; }
        public string TenantCode { get; private set; }

        public string UserName { get; private set; }
        public string FullName { get; private set; }

        public string Email { get; private set; }
        public bool ConfirmEmail { get; private set; }
        public string DialCode { get; private set; }
        public string PhoneNumber { get; private set; }

        public bool ConfirmPhoneNumber { get; private set; }

        public bool HasPaymentPassword { get; private set; }

        public string Role { get; private set; }

        public IReadOnlyCollection<string> Permissions { get; private set; }

        public bool HasPermission(string permissionCode)
        {
            return Permissions.Contains(permissionCode);
        }
    }
}