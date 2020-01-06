using System.Collections.Generic;
using System.Linq;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Admin
{
    public class UserIdentity
    {
        public UserIdentity(string userId,  string userName, string fullName,
            string email,
            string phoneNumber,
            string role, List<string> permissions)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.FullName = fullName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Role = role;
            this.Permissions = permissions != null ? permissions : new List<string>();
        }
        public string UserId { get; private set; }

        public string UserName { get; private set; }
        public string FullName { get; private set; }

        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public string Role { get; private set; }

        public IReadOnlyCollection<string> Permissions { get; private set; }

        public bool HasPermission(string permissionCode)
        {
            return Permissions.Contains(permissionCode);
        }
    }
}