using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Authorization.Client
{
    public interface IUserIdentityService
    {
        UserIdentity GetUserIdentity();
        Task<UserIdentity> GetUserIdentityAsync();


    }
}