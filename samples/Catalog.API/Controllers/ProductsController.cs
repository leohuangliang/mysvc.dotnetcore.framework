using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySvc.DotNetCore.Framework.Domain.Core;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus;
using Catalog.API.ViewModels;
using Catalog.Domain;
using Catalog.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDBContext _dbContext;
        private readonly IProductRepository _productRepository;

        public ProductsController(IDBContext dbContext, 
            IProductRepository productRepository)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{sku}")]
        public async Task<IActionResult> Get(string sku)
        {
            var product = await _productRepository.GetAsync(new MatchProductSKUSpecification(sku));
            return Ok(product?.SKU);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductViewModel createProductViewModel)
        {
            _dbContext.BeginTransaction();
            var product = new Product(createProductViewModel.SKU)
            {
                HeadLine = createProductViewModel.HeadLine
            };
            product.SetPrice(createProductViewModel.Price);
            await  _productRepository.AddAsync(product);

            await _dbContext.CommitAsync();
            return Ok();
        }

        // PUT 修改价格
        [HttpPut("{sku}/Price")]
        public async Task<IActionResult> Put(string sku, [FromBody] decimal newPrice)
        {
            _dbContext.BeginTransaction();
            var product = await _productRepository.GetAsync(new MatchProductSKUSpecification(sku));
            product.SetPrice(newPrice);
            await _productRepository.UpdateAsync(product);
            await _dbContext.CommitAsync();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
