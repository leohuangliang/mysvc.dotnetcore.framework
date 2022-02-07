namespace MySvc.Framework.IS4.Domain.Common.IdentityUser
{
    public class IdentityUserLogin
    {
        public IdentityUserLogin(string loginProvider, string providerKey, string providerDisplayName)
        {
            this.LoginProvider = loginProvider;
            this.ProviderDisplayName = providerDisplayName;
            this.ProviderKey = providerKey;
        }

        public IdentityUserLogin(UserLoginInfo login)
        {
            this.LoginProvider = login.LoginProvider;
            this.ProviderDisplayName = login.ProviderDisplayName;
            this.ProviderKey = login.ProviderKey;
        }

        public string LoginProvider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderKey { get; set; }

        public UserLoginInfo ToUserLoginInfo()
        {
            return new UserLoginInfo(this.LoginProvider, this.ProviderKey, this.ProviderDisplayName);
        }
    }
}
