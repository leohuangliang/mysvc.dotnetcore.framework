using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;
using Sample.Order.Domain.Repositories;

namespace Sample.Order.Application.Queries
{
    /// <summary>
    /// 订单查询器
    /// </summary>
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;

        private readonly ITypeAdapter _typeAdapter;

        public OrderQueries(IOrderReadOnlyRepository orderReadOnlyRepository, ITypeAdapter typeAdapter)
        {
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _typeAdapter = typeAdapter;
        }

        /// <summary>
        /// 根据订单号查询订单
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns>订单信息</returns>
        public async Task<ViewModels.Order> GetOrder(string orderNo)
        {
            var order = await _orderReadOnlyRepository.GetAsync(
                Specification<Domain.AggregatesModel.OrderAggregate.Order>.Eval(x => x.OrderNo == orderNo));

            return _typeAdapter.Adapt<ViewModels.Order>(order);
        }
    }
}
