using MySvc.DotNetCore.Framework.Domain.Core;

namespace Sample.Order.Domain.Repositories
{
    /// <summary>
    /// 订单信息，只读仓储接口
    /// </summary>
    public interface IOrderReadOnlyRepository : IReadOnlyRepository<AggregatesModel.OrderAggregate.Order>
    {
    }
}
