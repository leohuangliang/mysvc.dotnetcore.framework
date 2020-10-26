using MediatR;
using MySvc.DotNetCore.Framework.Domain.Core;
using Sample.Product.Domain.AggregatesModel.ProductAggregate.Specifications;
using Sample.Product.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Sample.Product.Application.Commands.Handlers
{
    /// <summary>
    /// 修改产品命令处理器
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IDBContext _dbContext;

        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IDBContext dbContext, IProductRepository productRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(new MatchProductBySKUSpecification(command.SKU));
            product.ChangeStockQty(command.StockQty);
            product.ChangeTitle(command.Title);
            product.Desc = command.Desc;

            _dbContext.BeginTransaction();
            await _productRepository.UpdateAsync(product);
            await _dbContext.CommitAsync();

            return new Unit();
        }
    }
}
