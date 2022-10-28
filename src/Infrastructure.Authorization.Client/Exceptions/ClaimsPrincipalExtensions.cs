using MySvc.Framework.Infrastructure.Crosscutting.Helpers;
using System.Security.Claims;

namespace MySvc.Framework.Infrastructure.Authorization.Client.Exceptions
{
    public static  class ClaimsPrincipalExtensions
    {
        public static string GetClaimValue(this ClaimsPrincipal user, string claimName)
        {
            string result = "";
            if (user != null && !claimName.IsNullOrBlank())
            {
                Claim claim = user.FindFirst(claimName);
                if (claim != null)
                {
                    result = claim.Value;
                }
            }

            return result;
        }
    }
}
