using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Merchant
{
    public interface IPermissionProvider
    {
        Task<List<string>> GetPermissionsAsync(string userId);
    }
}
