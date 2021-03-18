using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using System;
using System.Linq.Expressions;

namespace MySvc.DotNetCore.Framework.IS4.Domain.ApiResourceAggregate.Specifications
{
    public class MatchApiResourceByNameSpecification : Specification<ApiResource>
    {
        public string Name { get; private set; }

        public MatchApiResourceByNameSpecification(string name)
        {
            this.Name = name;
        }

        public override Expression<Func<ApiResource, bool>> GetExpression()
        {
            return x => x.Name == this.Name;
        }
    }
}
