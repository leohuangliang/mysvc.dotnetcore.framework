﻿using System;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Exceptions;

namespace MySvc.DotNetCore.Framework.Infrastructure.Authorization.Client.Extensions
{
    public class AuthValidationError : ExceptionBase
    {
        public AuthValidationError() { }

        public AuthValidationError(string errorCode, string message)
            : base(errorCode, message) { }

        public AuthValidationError(string errorCode, string message, Exception innerException)
            : base(errorCode, message, innerException) { }

        public override string Message => "ErrorCode:" + ErrorCode + "  Message: " + CustomMessage;
    }
}