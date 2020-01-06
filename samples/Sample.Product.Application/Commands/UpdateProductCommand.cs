using MediatR;

namespace Sample.Product.Application.Commands
{
    /// <summary>
    /// 修改产品的命令
    /// </summary>
    public class UpdateProductCommand : IRequest, ICommand
    {
        /// <summary>
        /// SKU 唯一标识
        /// </summary>
        public string SKU { get; private set; }

        /// <summary>
        /// 产品Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockQty { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string Desc { get; set; }

        public void SetSKU(string sku)
        {
            SKU = sku;
        }
    }
}
