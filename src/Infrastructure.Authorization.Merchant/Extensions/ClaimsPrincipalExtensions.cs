﻿using System.Security.Claims;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;

namespace MySvc.Framework.Infrastructure.Authorization.Merchant.Extensions
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
