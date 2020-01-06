using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB.Impl;
using MySvc.DotNetCore.Framework.Infrastructure.Data.MongoDB;
using Sample.Order.Domain.Repositories;

namespace Sample.Order.Repository.MongoDB
{
    /// <summary>
    /// 订单信息的只读查询仓储
    /// </summary>
    public class OrderReadOnlyRepository : ReadOnlyMongoDBRepository<Domain.AggregatesModel.OrderAggregate.Order>, IOrderReadOnlyRepository
    {
        public OrderReadOnlyRepository(IMongoDBContext context) : base(context)
        {

        }
    }
}
