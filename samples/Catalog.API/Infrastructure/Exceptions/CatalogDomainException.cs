using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Exceptions;

namespace Catalog.API.Infrastructure.Exceptions
{
    public class CatalogDomainException : ExceptionBase
    {
        public override string Message { get; }
    }
}
