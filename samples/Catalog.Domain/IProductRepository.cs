using System;
using System.Collections.Generic;
using System.Text;
using MySvc.DotNetCore.Framework.Domain.Core;

namespace Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
