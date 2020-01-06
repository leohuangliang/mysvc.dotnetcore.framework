using System.Threading.Tasks;

namespace Sample.Order.Application.Queries
{
    /// <summary>
    /// 产品信息查询器
    /// </summary>
    public interface IOrderQueries
    {
        /// <summary>
        /// 根据订单号查询订单
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns>订单信息</returns>
        Task<ViewModels.Order> GetOrder(string orderNo);
    }
}
