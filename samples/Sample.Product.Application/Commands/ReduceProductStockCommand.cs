using System.Collections.Generic;
using MediatR;

namespace Sample.Product.Application.Commands
{
    /// <summary>
    /// 减少产品库存的命令
    /// </summary>
    public class ReduceProductStockCommand : IRequest<bool>, ICommand
    {
        public ReduceProductStockCommand(IList<ReduceProductStockItem> reduceProductStockItems)
        {
            ReduceProductStockItems = reduceProductStockItems;
        }
        
        public IList<ReduceProductStockItem> ReduceProductStockItems { get; private set; }
    }

    public class ReduceProductStockItem
    {
        public ReduceProductStockItem(string sku, int qty)
        {
            SKU = sku;
            Qty = qty;
        }

        public string SKU { get; private set; }

        public int Qty { get; private set; }
    }
}
