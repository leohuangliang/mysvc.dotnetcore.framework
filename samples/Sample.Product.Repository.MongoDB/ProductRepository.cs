using System;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using Sample.Product.Domain.Repositories;

namespace Sample.Product.Repository.MongoDB
{
    /// <summary>
    /// 产品信息的仓储
    /// </summary>
    public class ProductRepository : MongoDBRepository<Domain.AggregatesModel.ProductAggregate.Product>, IProductRepository
    {
        public ProductRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
