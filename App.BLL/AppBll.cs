using App.BLL.Contracts;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.Contracts;
using Base.BLL;

namespace App.BLL;

public class AppBll : BaseBll<IAppUow>, IAppBll
{
    public AppBll(IAppUow uow) : base(uow)
    {
    }

    private ICategoryService? _categoryService;
    public ICategoryService CategoryService =>
        _categoryService ??= new CategoryService(
            BllUow, 
            new CategoryBllMapper());
    
    private IInventoryService? _inventoryService;
    public IInventoryService InventoryService =>
        _inventoryService ??= new InventoryService(
            BllUow, 
            new InventoryBllMapper());
    
    private IOrderProductService? _orderProductService;
    public IOrderProductService OrderProductService =>
        _orderProductService ??= new OrderProductService(
            BllUow, 
            new OrderProductBllMapper());
    
    private IOrderService? _orderService;
    public IOrderService OrderService =>
        _orderService ??= new OrderService(
            BllUow, 
            new OrderBllMapper());
    
    private IPaymentService? _paymentService;
    public IPaymentService PaymentService =>
        _paymentService ??= new PaymentService(
            BllUow, 
            new PaymentBllMapper());
    
    private IPersonService? _personService;
    public IPersonService PersonService =>
        _personService ??= new PersonService(
            BllUow, 
            new PersonBllMapper());
    
    private IProductService? _productService;
    public IProductService ProductService =>
        _productService ??= new ProductService(
            BllUow, 
            new ProductBllMapper());
    
    private IProductSupplierService? _productSupplierService;
    public IProductSupplierService ProductSupplierService =>
        _productSupplierService ??= new ProductSupplierService(
            BllUow, 
            new ProductSupplierBllMapper());
    
    private IRefundService? _refundService;
    public IRefundService RefundService =>
        _refundService ??= new RefundService(
            BllUow, 
            new RefundBllMapper());
    
    private IStockOrderItemService? _stockOrderItemService;
    public IStockOrderItemService StockOrderItemService =>
        _stockOrderItemService ??= new StockOrderItemService(
            BllUow, 
            new StockOrderItemBllMapper());
    
    private IStockOrderService? _stockOrderService;
    public IStockOrderService StockOrderService =>
        _stockOrderService ??= new StockOrderService(
            BllUow, 
            new StockOrderBllMapper());
    
    private ISupplierService? _supplierService;
    public ISupplierService SupplierService =>
        _supplierService ??= new SupplierService(
            BllUow, 
            new SupplierBllMapper());
    
    private IWarehouseService? _warehouseService;
    public IWarehouseService WarehouseService =>
        _warehouseService ??= new WarehouseService(
            BllUow, 
            new WarehouseBllMapper());
}