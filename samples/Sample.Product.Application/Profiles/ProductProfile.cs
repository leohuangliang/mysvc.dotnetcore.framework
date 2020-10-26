using AutoMapper;

namespace Sample.Product.Application.Profiles
{
    /// <summary>
    /// 产品类型的转换配置
    /// </summary>
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Domain.AggregatesModel.ProductAggregate.Product, ViewModels.Product>();
        }
    }
}
