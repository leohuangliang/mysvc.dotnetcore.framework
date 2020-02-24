﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core.Paged;
using MySvc.DotNetCore.Framework.Domain.Core.Specification;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Adapter;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using Sample.Product.Application.Queries.Criteria;
using Sample.Product.Domain.AggregatesModel.ProductAggregate.Specifications;
using Sample.Product.Domain.Repositories;

namespace Sample.Product.Application.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _productRepository;

        private readonly ITypeAdapter _typeAdapter;

        public ProductQueries(IProductRepository productRepository, ITypeAdapter typeAdapter)
        {
            _productRepository = productRepository;
            _typeAdapter = typeAdapter;
        }

        /// <summary>
        /// 根据SKU获取产品信息
        /// </summary>
        /// <param name="sku">sku信息</param>
        /// <returns>产品信息</returns>
        public async Task<ViewModels.Product> GetProductBySku(string sku)
        {
            var product = await _productRepository.GetAsync(new MatchProductBySKUSpecification(sku));
            return _typeAdapter.Adapt<ViewModels.Product>(product);
        }

        /// <summary>
        /// 分页查询产品信息
        /// </summary>
        /// <param name="criteria">查询条件</param>
        public async Task<PagedQueryResult<ViewModels.Product>> GetPagedList(ProductQueryCriteria criteria)
        {
            ISpecification<Sample.Product.Domain.AggregatesModel.ProductAggregate.Product> specification = Specification<Domain.AggregatesModel.ProductAggregate.Product>.Eval(x => true);

            if (criteria.Title.NotNullOrBlank())
            {
                specification = specification.And(new MatchProductByContainsTitleSpecification(criteria.Title));
            }

            //排序条件
            var sortOrder = new Dictionary<Expression<Func<Sample.Product.Domain.AggregatesModel.ProductAggregate.Product, dynamic>>, SortOrder>();
            sortOrder.Add(order => order.SKU, SortOrder.Ascending);

            var result = await _productRepository.FindInPageAsync(criteria.PageIndex, criteria.PageSize, specification, sortOrder);

            var products = new List<ViewModels.Product>();
            foreach (var product in result.Data)
            {
                products.Add(_typeAdapter.Adapt<ViewModels.Product>(product));
            }

            return new PagedQueryResult<ViewModels.Product>()
            {
                PageIndex = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
                TotalRecords = result.TotalRecords,
                Data = products
            };
        }
    }
}