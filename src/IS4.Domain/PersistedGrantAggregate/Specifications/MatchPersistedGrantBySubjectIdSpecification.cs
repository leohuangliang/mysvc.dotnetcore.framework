using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using System;
using System.Linq.Expressions;

namespace MySvc.DotNetCore.Framework.IS4.Domain.PersistedGrantAggregate.Specifications
{
    public class MatchPersistedGrantBySubjectIdSpecification : Specification<PersistedGrant>
    {
        public string SubjectId { get; private set; }

        public MatchPersistedGrantBySubjectIdSpecification(string subjectId)
        {
            this.SubjectId = subjectId;
        }
        public override Expression<Func<PersistedGrant, bool>> GetExpression()
        {
            return x => x.SubjectId == this.SubjectId;
        }
    }
}
