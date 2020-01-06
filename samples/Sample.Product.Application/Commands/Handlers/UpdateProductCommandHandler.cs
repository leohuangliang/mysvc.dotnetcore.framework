using System.Threading;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;
using MediatR;
using Sample.Product.Domain.AggregatesModel.ProductAggregate.Specifications;
using Sample.Product.Domain.Repositories;

namespace Sample.Product.Application.Commands.Handlers
{
    /// <summary>
    /// 修改产品命令处理器
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IDBContext _dbContext;

        private readonly IProductRepository _productRepository;

        private readonly ITypeAdapter _typeAdapter;

        public UpdateProductCommandHandler(IDBContext dbContext, IProductRepository productRepository, ITypeAdapter typeAdapter)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
            _typeAdapter = typeAdapter;
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
