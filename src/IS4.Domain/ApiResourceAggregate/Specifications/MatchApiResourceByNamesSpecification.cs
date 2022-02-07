using MySvc.Framework.Domain.Core.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MySvc.Framework.IS4.Domain.ApiResourceAggregate.Specifications
{
    public class MatchApiResourceByNamesSpecification : Specification<ApiResource>
    {
        public string[] Names { get; private set; }

        public MatchApiResourceByNamesSpecification(string[] names)
        {
            this.Names = names;
        }

        public override Expression<Func<ApiResource, bool>> GetExpression()
        {
            return x => this.Names.Contains(x.Name);
        }
    }
}
