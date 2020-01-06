using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client
{
    public interface IUserIdentityService
    {
        UserIdentity GetUserIdentity();
        Task<UserIdentity> GetUserIdentityAsync();


    }
}