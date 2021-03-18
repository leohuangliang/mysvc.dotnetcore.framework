using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using System;
using System.Linq.Expressions;

namespace MySvc.DotNetCore.Framework.IS4.Domain.PersistedGrantAggregate.Specifications
{
    public class MatchExpiredPersistedSpecification : Specification<PersistedGrant>
    {
        public override Expression<Func<PersistedGrant, bool>> GetExpression()
        {
            return x => x.Expiration < DateTime.UtcNow;
        }
    }
}
