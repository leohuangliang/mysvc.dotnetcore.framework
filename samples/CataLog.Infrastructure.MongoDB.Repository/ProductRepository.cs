using System;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using Catalog.Domain;
using MySvc.Framework.Infrastructure.Data.MongoDB;

namespace CataLog.Infrastructure.MongoDB.Repository
{
    public class ProductRepository : MongoDBRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
