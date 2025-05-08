using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAppBll : IBaseBll
{
    ICategoryService CategoryService { get; }
    IInventoryService InventoryService { get; }
    IOrderProductService OrderProductService { get; }
    IOrderService OrderService { get; }
    IPaymentService PaymentService { get; }
    IPersonService PersonService { get; }
    IProductService ProductService { get; }
    IProductSupplierService ProductSupplierService { get; }
    IRefundService RefundService { get; }
    IStockOrderItemService StockOrderItemService { get; }
    IStockOrderService StockOrderService { get; }
    ISupplierService SupplierService { get; }
    IWarehouseService WarehouseService { get; }
    
    /*IAccountService AccountService { get; }*/
}