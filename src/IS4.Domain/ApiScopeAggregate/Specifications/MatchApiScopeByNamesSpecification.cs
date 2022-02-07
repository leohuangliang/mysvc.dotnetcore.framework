using MySvc.Framework.Domain.Core.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MySvc.Framework.IS4.Domain.ApiScopeAggregate.Specifications
{
    public class MatchApiScopeByNamesSpecification : Specification<ApiScope>
    {
        private readonly string[] _names;

        public MatchApiScopeByNamesSpecification(string[] names)
        {
            _names = names ?? throw new ArgumentNullException(nameof(names));
        }

        public override Expression<Func<ApiScope, bool>> GetExpression()
        {
            return c => _names.Contains(c.Name);
        }
    }
}
