using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using System;
using System.Linq.Expressions;

namespace MySvc.DotNetCore.Framework.IS4.Domain.PersistedGrantAggregate.Specifications
{
    public class MatchPersistedGrantByKeySpecification : Specification<PersistedGrant>
    {
        public string Key { get; private set; }

        public MatchPersistedGrantByKeySpecification(string key)
        {
            this.Key = key;
        }
        public override Expression<Func<PersistedGrant, bool>> GetExpression()
        {
            return x => x.Key == this.Key;
        }
    }
}
