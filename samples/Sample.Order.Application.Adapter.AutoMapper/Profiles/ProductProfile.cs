using AutoMapper;

namespace Sample.Order.Application.Adapter.AutoMapper.Profiles
{
    /// <summary>
    /// 产品类型的转换配置
    /// </summary>
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Domain.AggregatesModel.OrderAggregate.Order, ViewModels.Order>();
            this.CreateMap<Domain.AggregatesModel.OrderAggregate.OrderItem, ViewModels.OrderItem>();
            this.CreateMap<Domain.AggregatesModel.OrderAggregate.ProductInfo, ViewModels.ProductInfo>();
        }
    }
}
