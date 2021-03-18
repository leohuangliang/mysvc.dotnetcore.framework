using MySvc.DotNetCore.Framework.Domain.Core.Impl;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.DotNetCore.Framework.IS4.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace MySvc.DotNetCore.Framework.IS4.Domain.Common.IdentityUser
{
    public abstract partial class IdentityUser<TLogin, TClaim, TToken> : AggregateRoot
        where TLogin : IdentityUserLogin
        where TClaim : IdentityUserClaim
        where TToken : IdentityUserToken, new()
    {
        private IdentityUser()
        {
            _roles = new List<string>();
            _logins = new List<TLogin>();
            _claims = new List<TClaim>();
            _tokens = new List<TToken>();
            this.LoginIpAddress = new List<string>();

        }

        public IdentityUser(string userName) : this()
        {
            if (userName.IsNullOrBlank()) throw new ArgumentNullException(nameof(userName));
            this.UserName = userName;
            UpdateNormalizedUserName();


        }

        /// <summary>
        /// 账号名称
        /// </summary>
        public string UserName { get; private set; }

        public string NormalizedUserName { get; private set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     A random value that must change whenever a users credentials change
        ///     (password changed, login removed)
        /// </summary>
        public string SecurityStamp { get; private set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; private set; }

        public string NormalizedEmail { get; private set; }

        public bool EmailConfirmed { get; private set; }

        public string PhoneNumber { get; protected set; }
        public string DialCode { get; private set; }


        public bool PhoneNumberConfirmed { get; protected set; }

        public bool TwoFactorEnabled { get; private set; } = false;

        public DateTime? LockoutEndDateUtc { get; private set; }

        public int MaxPasswordErrorRetryTimes
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// 是否已锁定账号
        /// </summary>
        public bool LockoutEnabled { get; private set; } = false;

        public bool IsDeleted { get; private set; }

        /// <summary>
        /// 登录失败次数
        /// </summary>
        public int AccessFailedCount { get; private set; }

        public string LoginProvider { get; private set; }

        private const string AuthenticatorKeyTokenName = "AuthenticatorKey";

        private const string RecoveryCodeTokenName = "RecoveryCodes";

        /// <summary>
        /// 最后登陆IP
        /// </summary>
        public string LastLoginIpAddress { get; protected set; }

        private List<string> _loginIpAddress;
        public List<string> LoginIpAddress
        {
            get
            {
                return _loginIpAddress ?? new List<string>();
            }
            protected set
            {
                _loginIpAddress = value;
            }
        }

        private List<string> _roles;

        public IReadOnlyCollection<string> Roles
        {
            get => _roles.AsReadOnly();
            private set => _roles = value.ToList();
        }

        public void SetRole(string role)
        {
            _roles.Clear();
            _roles.Add(role);
        }

        public string PasswordHash { get; private set; }

        private List<TLogin> _logins;

        public IReadOnlyCollection<TLogin> Logins
        {
            get => _logins.AsReadOnly();
            private set => _logins = value.ToList();
        }

        public void AddLogin(TLogin login)
        {
            _logins.Add(login);
        }

        public void RemoveLogin(string loginProvider, string providerKey)
        {
            _logins.RemoveAll(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
        }


        private List<TClaim> _claims;

        public IReadOnlyCollection<TClaim> Claims
        {
            get => _claims.AsReadOnly();
            private set => _claims = value.ToList();
        }

        public void AddClaim(TClaim claim)
        {
            _claims.Add(claim);
        }

        public void RemoveClaim(TClaim claim)
        {
            _claims.RemoveAll(c => c.Type == claim.Type && c.Value == claim.Value);
        }

        public void ReplaceClaim(TClaim existingClaim, TClaim newClaim)
        {
            var claimExists = this.Claims
                .Any(c => c.Type == existingClaim.Type && c.Value == existingClaim.Value);
            if (!claimExists)
            {
                // note: nothing to update, ignore, no need to throw
                return;
            }
            RemoveClaim(existingClaim);
            AddClaim(newClaim);
        }

        private List<TToken> _tokens;
        public IReadOnlyCollection<TToken> Tokens
        {
            get => _tokens.AsReadOnly();
            private set => _tokens = value.ToList();
        }

        private TToken GetToken(string loginProider, string name)
            => this.Tokens
                .FirstOrDefault(t => t.LoginProvider == loginProider && t.Name == name);

        public virtual void SetToken(string loginProider, string name, string value)
        {
            var existingToken = GetToken(loginProider, name);
            if (existingToken != null)
            {
                existingToken.Value = value;
                return;
            }

            _tokens.Add(new TToken()
            {
                LoginProvider = loginProider,
                Name = name,
                Value = value
            });
        }

        public string GetTokenValue(string loginProider, string name)
        {
            return GetToken(loginProider, name)?.Value;
        }

        public void RemoveToken(string loginProvider, string name)
        {
            _tokens.RemoveAll(t => t.LoginProvider == loginProvider && t.Name == name);
        }

        public override string ToString() => this.UserName;

        public void SetAuthenticatorKey(string key)
        {
            SetToken(this.LoginProvider, AuthenticatorKeyTokenName, key);
        }

        public string GetAuthenticatorKey()
        {
            return GetToken(this.LoginProvider, AuthenticatorKeyTokenName)?.Value;
        }

        public void ReplaceCodes(IEnumerable<string> recoveryCodes)
        {
            var mergedCodes = string.Join(";", recoveryCodes);
            SetToken(this.LoginProvider, RecoveryCodeTokenName, mergedCodes);
        }

        public bool RedeemCode(string code)
        {
            var mergedCodes = GetTokenValue(this.LoginProvider, RecoveryCodeTokenName) ?? string.Empty;
            var splitCodes = mergedCodes.Split(';');
            if (splitCodes.Contains(code))
            {
                var updatedCodes = new List<string>(splitCodes.Where(s => s != code));
                ReplaceCodes(updatedCodes);
                return true;
            }
            return false;
        }

        public virtual int CountCodes()
        {
            var mergedCodes = GetTokenValue(this.LoginProvider, RecoveryCodeTokenName) ?? string.Empty;
            if (mergedCodes.Length > 0)
            {
                return mergedCodes.Split(';').Length;
            }
            return 0;
        }

        public void SetPassword(string newPassword)
        {
            this.PasswordHash = HashPassword(newPassword);
            this.AccessFailedCount = 0;
        }



        public void SetEmail(string email)
        {
            if (this.Email.IsNullOrBlank())
            {
                this.Email = email;
                UpdateNormalizedEmail();
                this.EmailConfirmed = false;
            }
        }





        public void UpdateSecurityStamp()
        {
            RandomNumberGenerator _rng = RandomNumberGenerator.Create();

            byte[] bytes = new byte[20];
            _rng.GetBytes(bytes);
            this.SecurityStamp = Base32.ToBase32(bytes);
        }

        public void SetEmailConfirmed()
        {
            this.EmailConfirmed = true;
        }

        public void SetPhoneNumber(string dialCode, string phoneNumber)
        {
            if (dialCode.IsNullOrBlank()) throw new ArgumentNullException(nameof(dialCode));
            if (phoneNumber.IsNullOrBlank()) throw new ArgumentNullException(nameof(phoneNumber));

            if (phoneNumber.Contains("+"))
            {
                phoneNumber = phoneNumber.Replace($"+{dialCode}", "");
            }
            if (this.DialCode != dialCode || this.PhoneNumber != phoneNumber)
            {
                this.PhoneNumber = phoneNumber;
                this.PhoneNumberConfirmed = false;
                this.DialCode = dialCode;
                UpdateSecurityStamp();
            }
        }



        public void AccessFailed()
        {
            this.AccessFailedCount++;
            if (this.AccessFailedCount >= this.MaxPasswordErrorRetryTimes)
            {
                //锁定30分钟
                this.LockoutEndDateUtc = DateTime.UtcNow.Add(TimeSpan.FromMinutes(30));
                this.AccessFailedCount = 0;

            }
        }

        /// <summary>
        /// 访问成功
        /// </summary>
        public virtual void AccessSuccess(string ipAddressString)
        {
            this.AccessFailedCount = 0;
            this.LastLoginIpAddress = ipAddressString;
            if (this.LoginIpAddress == null) this.LoginIpAddress = new List<string>();
            if (!this.LoginIpAddress.Contains(ipAddressString))
            {
                if (this.LoginIpAddress.Count >= 20)
                {
                    this.LoginIpAddress.RemoveAt(0);
                }
                this.LoginIpAddress.Add(ipAddressString);
            }
        }

        /// <summary>
        /// 是否被锁定
        /// </summary>
        /// <returns></returns>
        public bool IsLockedOut()
        {
            //假如没有被锁定，返回false
            if (this.LockoutEnabled) return true;
            else
            {
                return this.LockoutEndDateUtc >= DateTime.UtcNow;
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="currentPassword">当前密码</param>
        /// <param name="newPassword"></param>
        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (VerifyPassword(currentPassword) != PasswordVerificationResult.Failed)
            {
                SetPassword(newPassword);
            }
            else
            {
                throw new IdentityDomainException(Error.Codes.PasswordMismatch, Error.Names.PasswordMismatch);
            }
        }

        /// <summary>
        /// 设置用户为已删除
        /// </summary>
        public void SetDeleted()
        {
            this.IsDeleted = true;
            this.UserName = $"{this.UserName}|{DateTime.UtcNow.Ticks}";
        }
        /// <summary>
        /// 更改用户状态
        /// </summary>
        public void SetState()
        {
            this.LockoutEnabled = !this.LockoutEnabled;

        }
        #region 私有方法


        private void UpdateNormalizedUserName()
        {
            var normalizedName = Normalize(this.UserName);
            this.NormalizedUserName = normalizedName;
        }

        private void UpdateNormalizedEmail()
        {
            var normalizedEmail = Normalize(this.Email);
            this.NormalizedEmail = normalizedEmail;
        }



        private string Normalize(string key)
        {
            return key.Normalize().ToUpperInvariant();
        }


        #endregion


    }


}
