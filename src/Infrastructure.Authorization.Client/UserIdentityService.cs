using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client.Extensions;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client
{
    public class UserIdentityService : IUserIdentityService
    {
        private IHttpContextAccessor _contextAccessor;
        private readonly IDistributedCache _distributedCache;
        private readonly IJsonConverter _jsonConverter;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserIdentityService> _logger;
        private readonly IOptions<AuthServiceOptions> _authServiceOptionsAccessor;

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

        public UserIdentity GetUserIdentity()
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthenticationException("unauthorized");
            }

            var tenantUser = MapTenantUser();
            return tenantUser;
        }

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
                    //string token = "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjBEOTJDQjNGNTY5RENBMzNGNzgzNUEwMDY4RkMzQzNCQUZEN0YzOEYiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJEWkxMUDFhZHlqUDNnMW9BYVB3OE82X1g4NDgifQ.eyJuYmYiOjE1NTg1OTE1MTksImV4cCI6MTU1ODYzNDcxOSwiaXNzIjoiaHR0cHM6Ly9hdXRoLnR5Y2VzLmNvbSIsImF1ZCI6WyJodHRwczovL2F1dGgudHljZXMuY29tL3Jlc291cmNlcyIsImNsaWVudCIsImNsaWVudElkZW50aXR5QXBpIl0sImNsaWVudF9pZCI6IndhbGxldC1qcy1jbGllbnQiLCJzdWIiOiIzYWNmNWY3OS00OTQ2LTQ2ZmQtYWZkNS05NTA4ZjQ5NTQ3YzciLCJhdXRoX3RpbWUiOjE1NTg1ODYwOTQsImlkcCI6ImxvY2FsIiwicHJlZmVycmVkX3VzZXJuYW1lIjoibGVvLmh1YW5nbGlhbmcyMDE1QGdtYWlsLmNvbSIsInVuaXF1ZV9uYW1lIjoibGVvLmh1YW5nbGlhbmcyMDE1QGdtYWlsLmNvbSIsInRlbmFudGNvZGUiOiI4MDMwNDQxMDExIiwicmVnaXN0ZXJmcm9tIjoiT25saW5lIiwicm9sZSI6IldhbGxldE93bmVyIiwiZW1haWwiOiJsZW8uaHVhbmdsaWFuZzIwMTVAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsImRpYWxjb2RlIjoiODYiLCJwaG9uZV9udW1iZXIiOiIxNTg5OTg1NjM1MCIsInBob25lX251bWJlcl92ZXJpZmllZCI6dHJ1ZSwiaGFzUGF5bWVudFBhc3N3b3JkIjp0cnVlLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiY2xpZW50IiwiY2xpZW50SWRlbnRpdHlBcGkiXSwiYW1yIjpbInB3ZCJdfQ.uUlZMH29Qml9NdBVy2gLTrt6tAR2MWu7yHIviLrJ5GVwX5lGYLiJRZN9mMDH3m5njXqqsjMNLzRCW6IeItrTTh5yp89Kb8MBywgqBm7KBVG1lSwJJDDfyY0cTrX86FXKGXCRu2r11uG3UzqQHuXZuoEpSoE4twhttDb0RlQ-vw1aTW-UHzU08gn4p1fCgM6075M1mbw5l0qPJuwDvaBjwZheNvYYDOENdmG_Y5GNNCPkt3bL0sTQVwmPIMQ2h-2nE4EhUM8jMK0gB1Tmgh8DQ29NKGoZNCM5ncbW96MWI52i9WMObAPeOjEOncjjmPIfodBwyGaYxzRT7kg3UROXXQ";
                    authClient.DefaultRequestHeaders.Add("Authorization", authorization.ToString());
                    //authClient.DefaultRequestHeaders.Add("Authorization", token);

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

            return new UserIdentity(tenantUserId, tenantCode, userProfile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private UserIdentity MapTenantUser()
        {


            string tenantUserId = _contextAccessor.HttpContext.User.GetClaimValue("sub");
            string tenantCode = _contextAccessor.HttpContext.User.GetClaimValue("tenantcode");
            string userName = _contextAccessor.HttpContext.User.GetClaimValue("unique_name");
            string role = _contextAccessor.HttpContext.User.GetClaimValue("role");
            string fullName = _contextAccessor.HttpContext.User.GetClaimValue("full_name");
            string email = _contextAccessor.HttpContext.User.GetClaimValue("email");
            string dialcode = _contextAccessor.HttpContext.User.GetClaimValue("dialcode");
            string phone_number = _contextAccessor.HttpContext.User.GetClaimValue("phone_number");
            string email_verified = _contextAccessor.HttpContext.User.GetClaimValue("email_verified");
            string phone_number_verified = _contextAccessor.HttpContext.User.GetClaimValue("phone_number_verified");
            string hasPaymentPassword = _contextAccessor.HttpContext.User.GetClaimValue("HasPaymentPassword");


            if (tenantUserId.IsNullOrBlank() || tenantCode.IsNullOrBlank() || userName.IsNullOrBlank())
            {
                throw new AuthenticationException("no login");
            }

            bool bool_email_verified = false;
            bool.TryParse(email_verified, out bool_email_verified);

            bool bool_phone_number_verified = false;
            bool.TryParse(phone_number_verified, out bool_phone_number_verified);

            bool bool_hasPaymentPassword = false;
            bool.TryParse(hasPaymentPassword, out bool_hasPaymentPassword);

            var userIdentity = new UserIdentity(tenantUserId, tenantCode, userName, fullName,
                email, bool_email_verified, dialcode, phone_number, bool_phone_number_verified, bool_hasPaymentPassword, role, new List<string>());

            _logger.LogDebug(_jsonConverter.SerializeObject(userIdentity));
            return userIdentity;
        }


    }
}