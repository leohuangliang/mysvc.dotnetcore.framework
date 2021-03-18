using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using System;
using System.Linq.Expressions;

namespace MySvc.DotNetCore.Framework.IS4.Domain.PersistedGrantAggregate.Specifications
{
    public class MatchPersistedGrantBySubjectIdAndClientIdSpecification : Specification<PersistedGrant>
    {
        public string ClientId { get; private set; }
        public string SubjectId { get; private set; }

        public MatchPersistedGrantBySubjectIdAndClientIdSpecification(string subjectId, string clientId)
        {
            this.SubjectId = subjectId;
            this.ClientId = clientId;
        }
        public override Expression<Func<PersistedGrant, bool>> GetExpression()
        {
            return x => x.SubjectId == this.SubjectId && x.ClientId == this.ClientId;
        }
    }
}
