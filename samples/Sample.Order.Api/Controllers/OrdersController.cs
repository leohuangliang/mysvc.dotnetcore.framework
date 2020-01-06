using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Order.Application.Commands;
using Sample.Order.Application.Queries;

namespace Sample.Order.Api.Controllers
{
    /// <summary>
    /// 订单信息API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueries _orderQueries;

        private readonly IMediator _mediator;

        public OrdersController(IOrderQueries orderQueries, IMediator mediator)
        {
            _orderQueries = orderQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderNo">订单编号</param>
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Application.ViewModels.Order))]
        public async Task<IActionResult> GetOrder(string orderNo)
        {
            var order = await _orderQueries.GetOrder(orderNo);

            return order !=null ? (IActionResult) this.Ok(order) : this.NotFound();
        }

        /// <summary>
        /// 创建订单信息
        /// </summary>
        /// <param name="command">创建订单的请求信息</param>
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Application.ViewModels.Order))]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }

        /// <summary>
        /// 注册账号测试
        /// </summary>
        /// <param name="command">注册账号测试</param>
        [HttpPost("Register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
        public async Task<IActionResult> RegisterTest([FromBody]TenantRegisterActivatedCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}