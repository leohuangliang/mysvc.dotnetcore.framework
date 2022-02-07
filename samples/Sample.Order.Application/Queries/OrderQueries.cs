using AutoMapper;
using MySvc.Framework.Domain.Core.Specification;
using Sample.Order.Domain.Repositories;
using System.Threading.Tasks;

namespace Sample.Order.Application.Queries
{
    /// <summary>
    /// 订单查询器RG
    /// </summary>
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;

        private readonly IMapper _mapper;

        public OrderQueries(IOrderReadOnlyRepository orderReadOnlyRepository, IMapper mapper)
        {
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _mapper = mapper;
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

            return _mapper.Map<ViewModels.Order>(order);
        }
    }
}
