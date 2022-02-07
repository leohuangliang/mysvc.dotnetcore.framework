using System;
using System.Runtime.Serialization;

namespace MySvc.Framework.Infrastructure.Crosscutting.Exceptions
{
    public class ConcurrencyException : ExceptionBase
    {
        public ConcurrencyException(string message)
            : base(ErrorCodes.StringCodes.ConcurrencyException, message)
        {

        }

        public ConcurrencyException(string message, Exception innerException) : base(ErrorCodes.StringCodes.ConcurrencyException, message, innerException)
        {
        }

        public ConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string Message
        {
            get { return "FrameWork:" + ErrorCode + "  Message: " + CustomMessage; }
        }
    }
}
