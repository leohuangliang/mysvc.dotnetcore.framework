using System.Security.Claims;

namespace MySvc.Framework.IS4.Domain.Common.IdentityUser
{
    /// <summary>
    /// A claim that a user possesses.
    /// </summary>
    public class IdentityUserClaim
    {
        public IdentityUserClaim()
        {
        }

        public IdentityUserClaim(Claim claim)
        {
            this.Type = claim.Type;
            this.Value = claim.Value;
        }

        /// <summary>
        /// Claim type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Claim value
        /// </summary>
        public string Value { get; set; }

        public Claim ToSecurityClaim()
        {
            return new Claim(this.Type, this.Value);
        }
    }
}
