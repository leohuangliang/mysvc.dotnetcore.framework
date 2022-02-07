using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Authorization.Merchant
{
    public interface IUserIdentityService
    {
        UserIdentity GetUserIdentity();
        Task<UserIdentity> GetUserIdentityAsync();


    }
}