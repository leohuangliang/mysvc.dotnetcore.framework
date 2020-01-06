using System;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using Sample.Order.Domain.Repositories;

namespace Sample.Order.Repository.MongoDB
{
    /// <summary>
    /// 订单信息的仓储
    /// </summary>
    public class OrderRepository : MongoDBRepository<Domain.AggregatesModel.OrderAggregate.Order>, IOrderRepository
    {
        public OrderRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
