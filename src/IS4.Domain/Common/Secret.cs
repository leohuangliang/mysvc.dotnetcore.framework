using System;

namespace MySvc.DotNetCore.Framework.IS4.Domain.Common
{
    public abstract class Secret
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = Consts.SecretTypes.SharedSecret;

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
