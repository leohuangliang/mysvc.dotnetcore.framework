using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MySvc.DotNetCore.Framework.IS4.Domain.ApiResourceAggregate.Specifications
{
    public class MatchApiResourceByScopeNamesSpecification : Specification<ApiResource>
    {
        public IEnumerable<string> ScopeNames { get; private set; }

        public MatchApiResourceByScopeNamesSpecification(IEnumerable<string> scopeNames)
        {
            this.ScopeNames = scopeNames;
        }

        public override Expression<Func<ApiResource, bool>> GetExpression()
        {
            return x => x.Scopes.Any(p => this.ScopeNames.Contains(p.Scope));
        }
    }
}
