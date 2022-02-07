using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Authorization.Admin
{
    public interface IPermissionProvider
    {
        Task<List<string>> GetPermissionsAsync(string userId);
    }
}
