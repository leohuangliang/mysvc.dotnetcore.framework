using AutoMapper;

namespace Sample.Order.Application.Adapter.AutoMapper.Profiles
{
    /// <summary>
    /// 订单类型的转换配置
    /// </summary>
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            this.CreateMap<Domain.AggregatesModel.OrderAggregate.Order, ViewModels.Order>();
            this.CreateMap<Domain.AggregatesModel.OrderAggregate.OrderItem, ViewModels.OrderItem>();
            this.CreateMap<Domain.AggregatesModel.OrderAggregate.ProductInfo, ViewModels.ProductInfo>();
            this.CreateMap<Domain.Common.Models.Address, Common.Models.Address>();
        }
    }
}
