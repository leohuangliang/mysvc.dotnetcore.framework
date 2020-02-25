//using System.Collections.Generic;
//using System.Security.Authentication;
//using System.Threading.Tasks;
//using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Json;
//using Microsoft.AspNetCore.Http;

//namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client
//{
//    /// <summary>
//    /// 对外开放Api获取当前用户
//    /// </summary>
//    public class UserIdentityOpenService : IUserIdentityService
//    {
//        private IHttpContextAccessor _contextAccessor;
//        private readonly IJsonConverter _jsonConverter;
//        public UserIdentityOpenService(IHttpContextAccessor contextAccessor, IJsonConverter jsonConverter)
//        {
//            _contextAccessor = contextAccessor;
//            _jsonConverter = jsonConverter;
//        }
//        public UserIdentity GetUserIdentity()
//        {
//            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
//            {
//                throw new AuthenticationException("unauthorized");
//            }

//            string tenantUserId = _contextAccessor.HttpContext.User.FindFirst("client_userid").Value;
//            string userName = _contextAccessor.HttpContext.User.FindFirst("client_username").Value;
//            string fullName = _contextAccessor.HttpContext.User.FindFirst("client_fullname").Value;
//            string email = _contextAccessor.HttpContext.User.FindFirst("client_email").Value;
//            bool confirmEmail = bool.Parse(_contextAccessor.HttpContext.User.FindFirst("client_confirmEmail").Value);

//            string tenantCode = _contextAccessor.HttpContext.User.FindFirst("client_tenantcode").Value;
//            string dialCode = _contextAccessor.HttpContext.User.FindFirst("client_dialcode").Value;
//            string phoneNumber = _contextAccessor.HttpContext.User.FindFirst("client_phoneNumber").Value;
//            bool confirmPhoneNumber = bool.Parse(_contextAccessor.HttpContext.User.FindFirst("client_confirmPhoneNumber").Value);
//            bool hasPaymentPasswrd = bool.Parse(_contextAccessor.HttpContext.User.FindFirst("client_hasPaymentPasswrd").Value);

//            var permissions = _jsonConverter.DeserializeObject<List<string>>(_contextAccessor.HttpContext.User.FindFirst("client_permissions").Value);
//            return new UserIdentity(tenantUserId, tenantCode, userName, fullName, 
//                email, confirmEmail, dialCode, phoneNumber, confirmPhoneNumber, hasPaymentPasswrd,  permissions);
//        }
//    }
//}
