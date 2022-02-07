﻿namespace MySvc.Framework.IS4.Domain.Common.IdentityUser
{
    /// <summary>
    ///     Authentication token associated with a user
    /// </summary>
    public class IdentityUserToken
    {
        /// <summary>
        /// The provider that the token came from.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// The name of the token.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value of the token.
        /// </summary>
        public string Value { get; set; }
    }
}
