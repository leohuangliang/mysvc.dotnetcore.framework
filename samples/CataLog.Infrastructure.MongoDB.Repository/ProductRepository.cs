using System;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using Catalog.Domain;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;

namespace CataLog.Infrastructure.MongoDB.Repository
{
    public class ProductRepository : MongoDBRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
