using MySvc.Framework.Domain.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MySvc.Framework.IS4.Domain.IdentityResourceAggregate.Specifications
{
    public class MatchIdentityResourceByScopeNamesSpecification : Specification<IdentityResource>
    {
        public IEnumerable<string> ScopeNames { get; private set; }

        public MatchIdentityResourceByScopeNamesSpecification(IEnumerable<string> scopeNames)
        {
            this.ScopeNames = scopeNames;
        }

        public override Expression<Func<IdentityResource, bool>> GetExpression()
        {
            return x => this.ScopeNames.Contains(x.Name);
        }
    }
}
