using System.Collections.Generic;
using System.Linq;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant
{
    public class UserIdentity
    {
        public UserIdentity(string tenantUserId, string tenantCode, string userName, string fullName,
            string email,bool confirmEmail,
            string dialCode,string phoneNumber, bool confirmPhoneNumber,
            string role, string clientId, bool hasPaymentPassword,string uid)
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
            this.HasPaymentPassword = hasPaymentPassword;
            this.UID = uid;
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
            this.HasPaymentPassword = userProfile.HasPaymentPassword;
            
        }

        public string TenantUserId { get;  init; }
        public string TenantCode { get; init; }
        public string TenantName { get; init; }
        public string ClientId { get; init; }

        public string UserName { get; init; }
        public string FullName { get; init; }

        public string Email { get; init; }
        public bool ConfirmEmail { get; init; }
        public string DialCode { get; init; }
        public string PhoneNumber { get; init; }

        public bool ConfirmPhoneNumber { get; init; }

        public string Role { get; init; }

        public bool HasPaymentPassword { get; init; }

        public string UID { get; init; }

    }
}