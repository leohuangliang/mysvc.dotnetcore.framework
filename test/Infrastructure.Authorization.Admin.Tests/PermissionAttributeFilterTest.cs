using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using MySvc.Framework.Infrastructure.Authorization.Admin;

namespace Infrastructure.Authorization.Admin.Tests
{
    public class PermissionAttributeFilterTest
    {
        [Fact]
        public void OnAuthorization_And_HasPermission_Test()
        {
            
            List<string> permissions = new () { "query-role", "edit-role" };
            PermissionsAuthorizationRequirement requirement = new(permissions);

            var userIdentityServiceMoq = new Mock<IUserIdentityService>();
            var permissionProviderMoq = new Mock<IPermissionProvider>();


            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                Mock.Of<ModelStateDictionary>()
            );

            var authorizationFilterContext = new AuthorizationFilterContext(
                actionContext,
                new List<IFilterMetadata>()
            );


            userIdentityServiceMoq.Setup(c => c.GetUserIdentity()).Returns(() =>
                new UserIdentity("leo", "hnhl@163.com", "leo", "hnhl@163.com", true, "", "", false, "operator",
                    new List<string>() {  }));

            permissionProviderMoq.Setup(c => c.GetPermissionsAsync("leo"))
                .ReturnsAsync(() => permissions);

            var permissionAttributeFilter = new PermissionAttributeFilter(requirement, userIdentityServiceMoq.Object,permissionProviderMoq.Object);

            permissionAttributeFilter.OnAuthorization(authorizationFilterContext);

            var result = authorizationFilterContext.Result as ForbidResult;

            Assert.Null(result);
        }

        [Fact]
        public void OnAuthorization_And_NotPermission_Test()
        {
            
            List<string> permissions = new () { "query-role", "edit-role" };
            PermissionsAuthorizationRequirement requirement = new(permissions);

            var userIdentityServiceMoq = new Mock<IUserIdentityService>();
            var permissionProviderMoq = new Mock<IPermissionProvider>();


            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                Mock.Of<ModelStateDictionary>()
            );

            var authorizationFilterContext = new AuthorizationFilterContext(
                actionContext,
                new List<IFilterMetadata>()
            );


            userIdentityServiceMoq.Setup(c => c.GetUserIdentity()).Returns(() =>
                new UserIdentity("leo", "hnhl@163.com", "leo", "hnhl@163.com", true, "", "", false, "operator",
                    new List<string>() {  }));

            permissionProviderMoq.Setup(c => c.GetPermissionsAsync("leo"))
                .ReturnsAsync(() =>  new List<string>(){ "query-role"});

            var permissionAttributeFilter = new PermissionAttributeFilter(requirement, userIdentityServiceMoq.Object,permissionProviderMoq.Object);

            permissionAttributeFilter.OnAuthorization(authorizationFilterContext);

            var result = authorizationFilterContext.Result as ForbidResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void OnAuthorization_Or_HasPermission_Test()
        {
            
            

            var userIdentityServiceMoq = new Mock<IUserIdentityService>();
            var permissionProviderMoq = new Mock<IPermissionProvider>();


            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                Mock.Of<ModelStateDictionary>()
            );

            var authorizationFilterContext = new AuthorizationFilterContext(
                actionContext,
                new List<IFilterMetadata>()
            );


            userIdentityServiceMoq.Setup(c => c.GetUserIdentity()).Returns(() =>
                new UserIdentity("leo", "hnhl@163.com", "leo", "hnhl@163.com", true, "", "", false, "operator",
                    new List<string>() {  }));

            permissionProviderMoq.Setup(c => c.GetPermissionsAsync("leo"))
                .ReturnsAsync(() => new List<string>(){ "query-role"});

            List<string> permissions = new () { "query-role", "edit-role" };
            PermissionsAuthorizationRequirement requirement = new(permissions, isOr:true);

            var permissionAttributeFilter = new PermissionAttributeFilter(requirement, userIdentityServiceMoq.Object,permissionProviderMoq.Object);

            permissionAttributeFilter.OnAuthorization(authorizationFilterContext);

            var result = authorizationFilterContext.Result as ForbidResult;

            Assert.Null(result);
        }

        [Fact]
        public void OnAuthorization_Or_NotPermission_Test()
        {
            
            

            var userIdentityServiceMoq = new Mock<IUserIdentityService>();
            var permissionProviderMoq = new Mock<IPermissionProvider>();


            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                Mock.Of<ModelStateDictionary>()
            );

            var authorizationFilterContext = new AuthorizationFilterContext(
                actionContext,
                new List<IFilterMetadata>()
            );


            userIdentityServiceMoq.Setup(c => c.GetUserIdentity()).Returns(() =>
                new UserIdentity("leo", "hnhl@163.com", "leo", "hnhl@163.com", true, "", "", false, "operator",
                    new List<string>() {  }));

            permissionProviderMoq.Setup(c => c.GetPermissionsAsync("leo"))
                .ReturnsAsync(() => new List<string>(){ "delete-role"});

            List<string> permissions = new () { "query-role", "edit-role" };
            PermissionsAuthorizationRequirement requirement = new(permissions, isOr:true);

            var permissionAttributeFilter = new PermissionAttributeFilter(requirement, userIdentityServiceMoq.Object,permissionProviderMoq.Object);

            permissionAttributeFilter.OnAuthorization(authorizationFilterContext);

            var result = authorizationFilterContext.Result as ForbidResult;

            Assert.NotNull(result);
        }
    }
}