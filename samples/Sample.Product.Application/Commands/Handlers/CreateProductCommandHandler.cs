using System;
using System.Threading;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;
using MediatR;
using Sample.Product.Domain.Repositories;
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

        private readonly ITypeAdapter _typeAdapter;

        public CreateProductCommandHandler(IDBContext dbContext, IProductRepository productRepository, ITypeAdapter typeAdapter)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
            _typeAdapter = typeAdapter;
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

            return _typeAdapter.Adapt<ViewModels.Product>(product);
        }
    }
}
