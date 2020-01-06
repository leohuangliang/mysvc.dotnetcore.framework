using System.Collections.Generic;
using System.Linq;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant
{
    public class UserIdentity
    {
        public UserIdentity(string tenantUserId, string tenantCode, string userName, string fullName,
            string email,bool confirmEmail,
            string dialCode,string phoneNumber, bool confirmPhoneNumber,
            string role, string clientId)
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
            this.Role = role;
            this.ClientId = clientId;
        }

        public UserIdentity(string tenantUserId, string tenantCode, string clientId, UserProfile userProfile)
        {
            this.TenantUserId = tenantUserId;
            this.TenantCode = tenantCode;
            this.ClientId = clientId;
            this.UserName = userProfile.UserName;
            this.FullName = userProfile.FullName;
            this.Email = userProfile.Email;
            this.ConfirmEmail = userProfile.EmailConfirmed;
            this.DialCode = userProfile.DialCode;
            this.PhoneNumber = userProfile.PhoneNumber;
            this.ConfirmPhoneNumber = userProfile.PhoneNumberConfirmed;
            this.Role = userProfile.Role;
        }

        public string TenantUserId { get; private set; }
        public string TenantCode { get; private set; }

        public string ClientId { get; private set; }

        public string UserName { get; private set; }
        public string FullName { get; private set; }

        public string Email { get; private set; }
        public bool ConfirmEmail { get; private set; }
        public string DialCode { get; private set; }
        public string PhoneNumber { get; private set; }

        public bool ConfirmPhoneNumber { get; private set; }

        public string Role { get; private set; }
    }
}