using System.Threading;
using System.Threading.Tasks;
using MySvc.Framework.Domain.Core;
using MediatR;
using Sample.Product.Domain.AggregatesModel.ProductAggregate.Specifications;
using Sample.Product.Domain.Repositories;

namespace Sample.Product.Application.Commands.Handlers
{
    /// <summary>
    /// 减少产品库存的命令处理器
    /// </summary>
    public class ReduceProductStockCommandHandler : IRequestHandler<ReduceProductStockCommand, bool>
    {
        private readonly IDBContext _dbContext;

        private readonly IProductRepository _productRepository;

        public ReduceProductStockCommandHandler(IDBContext dbContext, IProductRepository productRepository)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(ReduceProductStockCommand request, CancellationToken cancellationToken)
        {
            if (request.ReduceProductStockItems != null)
            {
                _dbContext.BeginTransaction();
                foreach (var item in request.ReduceProductStockItems)
                {
                    var product = await _productRepository.GetAsync(new MatchProductBySKUSpecification(item.SKU));
                    product.DeductingStockQty(item.Qty); //扣减库存

                    await _productRepository.UpdateAsync(product);
                }

                await _dbContext.CommitAsync();
            }

            return true;
        }
    }
}
