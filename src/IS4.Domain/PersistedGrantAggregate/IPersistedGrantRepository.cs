using MySvc.DotNetCore.Framework.Domain.Core;
using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.IS4.Domain.PersistedGrantAggregate
{
    public interface IPersistedGrantRepository : IRepository<PersistedGrant>
    {
        Task RemoveExpired();
    }
}
