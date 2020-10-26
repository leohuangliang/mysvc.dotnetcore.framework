using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant.Exceptions;
using MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant.Extensions;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client.Exceptions;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant
{
    /// <summary>
    /// 
    /// </summary>
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDistributedCache _distributedCache;
        private readonly IJsonConverter _jsonConverter;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserIdentityService> _logger;
        private readonly IOptions<AuthServiceOptions> _authServiceOptionsAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="distributedCache"></param>
        /// <param name="jsonConverter"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name="logger"></param>
        /// <param name="authServiceOptionsAccessor"></param>
        public UserIdentityService(
            IHttpContextAccessor contextAccessor,
            IDistributedCache distributedCache,
            IJsonConverter jsonConverter,
            IHttpClientFactory httpClientFactory,
            ILogger<UserIdentityService> logger,

            IOptions<AuthServiceOptions> authServiceOptionsAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            _jsonConverter = jsonConverter ?? throw new ArgumentNullException(nameof(jsonConverter));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            _logger = logger;
            _authServiceOptionsAccessor = authServiceOptionsAccessor ?? throw new ArgumentNullException(nameof(authServiceOptionsAccessor));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AuthenticationException"></exception>
        public UserIdentity GetUserIdentity()
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthenticationException("unauthorized");
            }

            var tenantUser = MapTenantUser();
            return tenantUser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AuthenticationException"></exception>
        /// <exception cref="AuthValidationError"></exception>
        public async Task<UserIdentity> GetUserIdentityAsync()
        {

            if (_contextAccessor.HttpContext == null)
            {
                _logger.LogError("_contextAccessor.HttpContext is null");
                throw new ArgumentNullException(nameof(_contextAccessor.HttpContext));
            }

            if (_contextAccessor.HttpContext.User == null)
            {
                _logger.LogError("_contextAccessor.HttpContext.User is null");
                throw new ArgumentNullException(nameof(_contextAccessor.HttpContext.User));
            }
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthenticationException("unauthorized");
            }

            string tenantUserId = _contextAccessor.HttpContext.User.GetClaimValue("sub");
            string tenantCode = _contextAccessor.HttpContext.User.GetClaimValue("tenantcode");

            string clientId = _contextAccessor.HttpContext.User.GetClaimValue("client_id");

            if (tenantCode.IsNullOrBlank())
            {
                tenantCode = _contextAccessor.HttpContext.User.GetClaimValue("client_tenantcode");
            }

            string cacheKey = $"user_profile_{tenantUserId}";
            UserProfile userProfile = null;
            var json = await _distributedCache.GetStringAsync(cacheKey);
            if (!json.IsNullOrBlank())
            {
                userProfile = _jsonConverter.DeserializeObject<UserProfile>(json);
            }
            else
            {
                try
                {
                    _contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorization);
                    var authClient = _httpClientFactory.CreateClient("authService");
                    if (authClient == null)
                    {
                        throw new ArgumentNullException(nameof(authClient), "authservice_httpClient_Config_NoFound");
                    }

                    authClient.DefaultRequestHeaders.Add("Authorization", authorization.ToString());

                    _logger.LogInformation($"获取鉴权信息(BaseAddress:{authClient.BaseAddress}, Path:{_authServiceOptionsAccessor.Value.GetUserProfilePath}, Token:{authorization.ToString()}");
                    HttpResponseMessage response = await authClient.GetAsync(_authServiceOptionsAccessor.Value.GetUserProfilePath);
                    string stringData = _jsonConverter.SerializeObject(response);
                    _logger.LogInformation($"Get User Profile:{stringData}");
                    if (response.IsSuccessStatusCode)
                    {
                        stringData = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation($"GetUserProfile：{stringData}");
                        userProfile = _jsonConverter.DeserializeObject<UserProfile>(stringData);
                    }
                    else
                    {
                        stringData = _jsonConverter.SerializeObject(response);
                        _logger.LogError(stringData);
                        throw new AuthValidationError(Error.Codes.RequestUserProfileFailed, Error.Names.RequestUserProfileFailed);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
                    throw new AuthValidationError(Error.Codes.RequestUserProfileFailed, Error.Names.RequestUserProfileFailed, ex);
                }
            }

            if (userProfile == null) throw new AuthValidationError(Error.Codes.RequestUserProfileFailed, Error.Names.RequestUserProfileFailed);

            return new UserIdentity(tenantUserId, tenantCode, clientId, userProfile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private UserIdentity MapTenantUser()
        {

            string clientId = _contextAccessor.HttpContext.User.GetClaimValue("client_id");
            string tenantUserId = _contextAccessor.HttpContext.User.GetClaimValue("sub");
            string tenantCode = _contextAccessor.HttpContext.User.GetClaimValue("tenantcode");

            if (tenantCode.IsNullOrBlank())
            {
                tenantCode = _contextAccessor.HttpContext.User.GetClaimValue("client_tenantcode");
            }

            string userName = _contextAccessor.HttpContext.User.GetClaimValue("unique_name");
            string role = _contextAccessor.HttpContext.User.GetClaimValue("role");
            string fullName = _contextAccessor.HttpContext.User.GetClaimValue("full_name");
            string email = _contextAccessor.HttpContext.User.GetClaimValue("email");
            string dialcode = _contextAccessor.HttpContext.User.GetClaimValue("dialcode");
            string phone_number = _contextAccessor.HttpContext.User.GetClaimValue("phone_number");
            string email_verified = _contextAccessor.HttpContext.User.GetClaimValue("email_verified");
            string phone_number_verified = _contextAccessor.HttpContext.User.GetClaimValue("phone_number_verified");

            if (tenantCode.IsNullOrBlank())
            {
                throw new AuthenticationException("no login");
            }

            bool bool_email_verified = false;
            bool.TryParse(email_verified, out bool_email_verified);

            bool bool_phone_number_verified = false;
            bool.TryParse(phone_number_verified, out bool_phone_number_verified);

            var userIdentity = new UserIdentity(tenantUserId, tenantCode, userName, fullName,
                email, bool_email_verified, dialcode, phone_number, bool_phone_number_verified, role, clientId);

            _logger.LogDebug(_jsonConverter.SerializeObject(userIdentity));
            return userIdentity;
        }


    }
}