namespace MySvc.DotNetCore.Framework.IS4.Domain
{
    public class Consts
    {
        public static class SecretTypes
        {
            public const string SharedSecret = "SharedSecret";
            public const string X509CertificateThumbprint = "X509Thumbprint";
            public const string X509CertificateName = "X509Name";
            public const string X509CertificateBase64 = "X509CertificateBase64";
        }

        public static class ProtocolTypes
        {
            public const string OpenIdConnect = "oidc";
            public const string WsFederation = "wsfed";
            public const string Saml2p = "saml2p";
        }

        public static class Role
        {
            /// <summary>
            /// 系统管理员
            /// </summary>
            public const string Admin = "Admin";

        }
    }
}