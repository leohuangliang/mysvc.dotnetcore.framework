using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using System;
using System.Linq.Expressions;

namespace MySvc.DotNetCore.Framework.IS4.Domain.PersistedGrantAggregate.Specifications
{
    public class MatchPersistedGrantByFilterSpecification : Specification<PersistedGrant>
    {
        public string ClientId { get; private set; }
        public string SessionId { get; }
        public string Type { get; private set; }
        public string SubjectId { get; private set; }

        public MatchPersistedGrantByFilterSpecification(string subjectId, string clientId, string sessionId, string type)
        {
            this.SubjectId = subjectId;
            this.ClientId = clientId;
            this.SessionId = sessionId;
            this.Type = type;
        }
        public override Expression<Func<PersistedGrant, bool>> GetExpression()
        {
            Specification<PersistedGrant> specification = new AnySpecification<PersistedGrant>();
            if (!this.SubjectId.IsNullOrBlank())
            {
                specification = new AndSpecification<PersistedGrant>(specification, new MatchPersistedGrantBySubjectIdSpecification(this.SubjectId));
            }

            if (!this.ClientId.IsNullOrBlank())
            {
                specification = new AndSpecification<PersistedGrant>(specification, Specification<PersistedGrant>.Eval(c => c.ClientId == this.ClientId));
            }

            if (!this.SessionId.IsNullOrBlank())
            {
                specification = new AndSpecification<PersistedGrant>(specification, Specification<PersistedGrant>.Eval(c => c.SessionId == this.SessionId));
            }

            if (!this.Type.IsNullOrBlank())
            {
                specification = new AndSpecification<PersistedGrant>(specification, Specification<PersistedGrant>.Eval(c => c.Type == this.Type));
            }

            return specification.GetExpression();
        }
    }
}
