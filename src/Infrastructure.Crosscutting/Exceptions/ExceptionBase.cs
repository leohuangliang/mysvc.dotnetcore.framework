using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Crosscutting.Exceptions
{

    public abstract class ExceptionBase : Exception
    {
        protected ExceptionBase()
        {
        }

#pragma warning disable SYSLIB0051 // Type or member is obsolete
        protected ExceptionBase(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#pragma warning restore SYSLIB0051 // Type or member is obsolete

        protected ExceptionBase(string errorCode)
        {
            Init(errorCode, string.Empty);
        }

        protected ExceptionBase(string errorCode, Exception innerException)
            : base(string.Empty, innerException)
        {
            Init(errorCode, string.Empty);
        }

        protected ExceptionBase(string errorCode, string message)
        {
            Init(errorCode, message);
        }

        protected ExceptionBase(string errorCode, string message, Exception innerException)
            : base(null, innerException)
        {
            Init(errorCode, message);
        }

        protected ExceptionBase(string errorCode, string messageFormat, params object[] messageArgs)
        {
            Init(errorCode, String.Format(messageFormat, messageArgs));
        }

        protected ExceptionBase(string errorCode, Exception innerException, string messageFormat, params object[] messageArgs)
            : base(null, innerException)
        {
            Init(errorCode, String.Format(messageFormat, messageArgs));
        }

        void Init(string errorCode, string message)
        {
            ErrorCode = errorCode;
            CustomMessage = message;
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string? ErrorCode
        {
            get;
            private set;
        }


        public string? CustomMessage { get; private set; }

        public abstract override string Message
        {
            get;
        }
    }
}
