using MySvc.DotNetCore.Framework.Domain.Core;

namespace Sample.Order.Domain.Repositories
{
    /// <summary>
    /// 订单仓储接口
    /// </summary>
    public interface IOrderRepository : IRepository<AggregatesModel.OrderAggregate.Order>
    {
    }
}
