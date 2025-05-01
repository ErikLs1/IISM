using App.BLL.Contracts;
using App.BLL.Services;
using App.DAL.EF;
using Base.BLL;

namespace App.BLL;

public class AppBll : BaseBll<AppUow>, IAppBll
{
    public AppBll(AppUow bllUow) : base(bllUow)
    {
    }

    private ICategoryService _categoryService;

    public ICategoryService CategoryService =>
        _categoryService ??= new CategoryService(BllUow, BllUow.CategoryRepository, );
    
    private IInventoryService _inventoryService;
    private IOrderProductService _orderProductService;
    private IOrderService _orderService;
    private IPaymentService _paymentService;
    private IPersonService _personService;
    private IProductService _productService;
    private IProductSupplierService _productSupplierService;
    private IRefundService _refundService;
    private IStockOrderItemService _stockOrderItemService;
    private IStockOrderService _stockOrderService;
    private ISupplierService _supplierService;
    private IWarehouseService _warehouseService;
}