using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant
{
    public interface IUserIdentityService
    {
        UserIdentity GetUserIdentity();
        Task<UserIdentity> GetUserIdentityAsync();


    }
}