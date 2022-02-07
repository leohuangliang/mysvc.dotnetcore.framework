using AutoMapper;
using MediatR;
using MySvc.Framework.Domain.Core;
using Sample.Product.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using DomainProduct = Sample.Product.Domain.AggregatesModel.ProductAggregate;

namespace Sample.Product.Application.Commands.Handlers
{
    /// <summary>
    /// 创建产品命令处理器
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ViewModels.Product>
    {
        private readonly IDBContext _dbContext;

        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IDBContext dbContext, IProductRepository productRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ViewModels.Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new DomainProduct.Product(command.SKU, command.Title, command.StockQty)
            {
                Desc = command.Desc
            };

            _dbContext.BeginTransaction();
            await _productRepository.AddAsync(product);
            
            await _dbContext.CommitAsync();

            return _mapper.Map<ViewModels.Product>(product);
        }
    }
}
