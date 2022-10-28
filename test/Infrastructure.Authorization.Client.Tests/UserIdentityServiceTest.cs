using MySvc.Framework.Infrastructure.Authorization.Client;
using Moq;
using System;
using System.Net.Http;
using System.Security.Principal;
using Xunit;
using Xunit.Abstractions;
using Moq.Contrib.HttpClient;
namespace Infrastructure.Authorization.Client.Tests
{
    public class UserIdentityServiceTest
    {
        private readonly ITestOutputHelper _output;

        public UserIdentityServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public async void Test1()
        {
            //var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            //var httpContextMock = new Mock<HttpContext>();
            //var claimsPrincipalMock = new Mock<ClaimsPrincipal>();
            //var iIdentityMock = new Mock<IIdentity>();
            //iIdentityMock.Setup(c => c.IsAuthenticated).Returns(true);

            //claimsPrincipalMock.Setup(c => c.Identity).Returns(iIdentityMock.Object);
            //claimsPrincipalMock.Setup(c => c.FindFirst("sub").Value).Returns("ed095ea6-fd4a-438b-a35a-4919ae869435");
            //claimsPrincipalMock.Setup(c => c.FindFirst("tenantcode").Value).Returns("9036918604");
            //httpContextMock.Setup(c => c.User).Returns(claimsPrincipalMock.Object);

            //httpContextAccessorMock.Setup(c => c.HttpContext).Returns(httpContextMock.Object);
            //httpContextAccessorMock.Setup(c => c.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorization)).Returns(httpContextMock.Object);

            //UserIdentityService userIdentityService = new UserIdentityService(httpContextAccessorMock.Object,);
            var handler = new Mock<HttpMessageHandler>();
            var factory = handler.CreateClientFactory();


            Mock.Get(factory).Setup(x => x.CreateClient("authService"))
            .Returns(() =>
            {
                var client = HttpClientFactory.Create();
                client.BaseAddress = new Uri("http://localhost:5001");
                return client;
            });
            var authClient = factory.CreateClient("authService");
            authClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjQ3OWMwYTJkNzA4ZjU1MTcwMTRkYzllOTQ1MzM3YzkyIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NTg1MjM0MzUsImV4cCI6MTU1ODUyNzAzNSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJjbGllbnRJZGVudGl0eUFwaSJdLCJjbGllbnRfaWQiOiJDbGllbnRJZGVudGl0eVNlcnZpY2VTd2FnZ2VyVUkiLCJzdWIiOiJlZDA5NWVhNi1mZDRhLTQzOGItYTM1YS00OTE5YWU4Njk0MzUiLCJhdXRoX3RpbWUiOjE1NTg1MDkwNDEsImlkcCI6ImxvY2FsIiwicHJlZmVycmVkX3VzZXJuYW1lIjoibGVvLmh1YW5nbGlhbmcyMDE1QGdtYWlsLmNvbSIsInVuaXF1ZV9uYW1lIjoibGVvLmh1YW5nbGlhbmcyMDE1QGdtYWlsLmNvbSIsInRlbmFudGNvZGUiOiI5MDM2OTE4NjA0IiwicmVnaXN0ZXJmcm9tIjoiT25saW5lIiwiZnVsbF9uYW1lIjoibGVvIiwicm9sZSI6IldhbGxldE93bmVyIiwiZW1haWwiOiJsZW8uaHVhbmdsaWFuZzIwMTVAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsImRpYWxjb2RlIjoiODYiLCJwaG9uZV9udW1iZXIiOiIxODU3NjY4OTU4MCIsInBob25lX251bWJlcl92ZXJpZmllZCI6dHJ1ZSwiaGFzUGF5bWVudFBhc3N3b3JkIjpmYWxzZSwic2NvcGUiOlsiY2xpZW50SWRlbnRpdHlBcGkiXSwiYW1yIjpbInB3ZCJdfQ.u_xsIqvxrRaf1grKhkFqGGp_QL3IT-AsJk5qSvlQVgjy2TQVX941QF8a1VPeK-WTFkNWyqiEy04Ciq5fz-VBshbvm1Q7Lnt9nM9G-EYQn4Gb5NanK9yuA3ad-sqnlKWq2-edz4o7-htMR-pq9koV4_Lyh8fpM2ZvN_bms_SpgbvPrrb8sviB-ipJr5iPtbFcL0losv7vNmAIBc5C6b8HPIt9lPVUfdPsGc1G-dY-CyP0otFnzWVBpobzPC8ydGAYaCzPGWXicuj5DfRhoadEdvoUcSMSjfhrNoLdOfc_fcfQR5IVAahwnZmemqSNnK3a1CYxNvK_yeDBmALbQ8-wHQ");

            HttpResponseMessage response = await authClient.GetAsync("/api/userSetting/profile");
            UserProfile userProfile = null;
            if (response.IsSuccessStatusCode)
            {
                string stringData = await response.Content.ReadAsStringAsync();

                userProfile = Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(stringData);
            }
            else
            {
                string stringData = await response.Content.ReadAsStringAsync();

            }
        }
    }
}
