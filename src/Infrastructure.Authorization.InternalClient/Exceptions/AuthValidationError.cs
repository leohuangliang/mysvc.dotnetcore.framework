using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Exceptions;
using System;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.InternalClient.Exceptions
{
    public class AuthValidationError : ExceptionBase
    {
        public AuthValidationError() { }

        public AuthValidationError(string errorCode, string message)
            : base(errorCode, message) { }

        public AuthValidationError(string errorCode, string message, Exception innerException)
            : base(errorCode,message, innerException) { }

        public override string Message => "ErrorCode:" + this.ErrorCode + "  Message: " + this.CustomMessage;
    }
}
