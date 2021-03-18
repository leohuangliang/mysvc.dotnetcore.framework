using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using System;
using System.Linq.Expressions;

namespace MySvc.DotNetCore.Framework.IS4.Domain.PersistedGrantAggregate.Specifications
{
    public class MatchPersistedGrantBySubjectIdAndClientIdAndTypeSpecification : Specification<PersistedGrant>
    {
        public string ClientId { get; private set; }
        public string Type { get; private set; }
        public string SubjectId { get; private set; }

        public MatchPersistedGrantBySubjectIdAndClientIdAndTypeSpecification(string subjectId, string clientId, string type)
        {
            this.SubjectId = subjectId;
            this.ClientId = clientId;
            this.Type = type;
        }
        public override Expression<Func<PersistedGrant, bool>> GetExpression()
        {
            return x => x.SubjectId == this.SubjectId && x.ClientId == this.ClientId && x.Type == this.Type;
        }
    }
}
