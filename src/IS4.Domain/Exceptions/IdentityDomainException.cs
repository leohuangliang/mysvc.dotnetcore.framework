using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Exceptions;

namespace MySvc.DotNetCore.Framework.IS4.Domain.Exceptions
{
    public class IdentityDomainException : ExceptionBase
    {
        public IdentityDomainException(string errorCode, string message) : base(errorCode, message)
        {

        }

        public override string Message => $"IdentityDomain Exception:{this.ErrorCode} Message:{this.CustomMessage}";
    }
}
