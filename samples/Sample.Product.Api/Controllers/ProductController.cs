using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Product.Application.Commands;
using Sample.Product.Application.Queries;
using Sample.Product.Application.Queries.Criteria;

namespace Sample.Product.Api.Controllers
{
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductQueries _productQueries;

        private readonly IMediator _mediator;

        public ProductController(IProductQueries productQueries, IMediator mediator)
        {
            _productQueries = productQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// 根据SKU获取产品信息
        /// </summary>
        /// <param name="sku">产品SKU</param>
        [HttpGet]
        [Route("{sku}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Application.ViewModels.Product))]
        public async Task<IActionResult> GetProduct([FromRoute] string sku)
        {
            var product = await _productQueries.GetProductBySku(sku);
            return Ok(product);
        }

        /// <summary>
        /// 分页查询产品信息
        /// </summary>
        /// <param name="criteria">查询条件</param>
        [HttpGet]
        [Route("pagedResult")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedQueryResult<Application.ViewModels.Product>))]
        public async Task<IActionResult> GetProduct([FromQuery] ProductQueryCriteria criteria)
        {
            var result = await _productQueries.GetPagedList(criteria);
            return Ok(result);
        }

        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="command">创建产品的命令</param>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Application.ViewModels.Product))]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return Ok(product);
        }

        /// <summary>
        /// 修改产品信息
        /// </summary>
        /// <param name="sku">产品SKU</param>
        /// <param name="command">修改产品的命令</param>
        [HttpPut]
        [Route("{sku}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromRoute] string sku, UpdateProductCommand command)
        {
            command.SetSKU(sku);
            await _mediator.Send(command);
            return Ok();
        }
    }
}