using MySvc.Framework.Domain.Core;
using System.Threading.Tasks;

namespace MySvc.Framework.IS4.Domain.PersistedGrantAggregate
{
    public interface IPersistedGrantRepository : IRepository<PersistedGrant>
    {
        Task RemoveExpired();
    }
}
