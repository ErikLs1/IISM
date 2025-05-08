using App.DAL.Contracts;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUow : BaseUow<AppDbContext>, IAppUow
{
    public AppUow(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }

    private ICategoryRepository? _categoryRepository;
    public ICategoryRepository CategoryRepository =>
        _categoryRepository ??= new CategoryRepository(UowDbContext);

    private IInventoryRepository? _inventoryRepository;
    public IInventoryRepository InventoryRepository =>
        _inventoryRepository ??= new InventoryRepository(UowDbContext);
    
    private IOrderProductRepository? _orderProductRepository;
    public IOrderProductRepository OrderProductRepository =>
        _orderProductRepository ??= new OrderProductRepository(UowDbContext);
    
    private IOrderRepository? _orderRepository;
    public IOrderRepository OrderRepository =>
        _orderRepository ??= new OrderRepository(UowDbContext);
    
    private IPaymentRepository? _paymentRepository;
    public IPaymentRepository PaymentRepository =>
        _paymentRepository ??= new PaymentRepository(UowDbContext);
    
    private IPersonRepository? _personRepository;
    public IPersonRepository PersonRepository =>
        _personRepository ??= new PersonRepository(UowDbContext);
    
    private IProductRepository? _productRepository;
    public IProductRepository ProductRepository =>
        _productRepository ??= new ProductRepository(UowDbContext);
    
    private IProductSupplierRepository? _productSupplierRepository;
    public IProductSupplierRepository ProductSupplierRepository =>
        _productSupplierRepository ??= new ProductSupplierRepository(UowDbContext);
    
    private IRefundRepository? _refundRepository;
    public IRefundRepository RefundRepository =>
        _refundRepository ??= new RefundRepository(UowDbContext);
    
    private IStockOrderItemRepository? _stockOrderItemRepository;
    public IStockOrderItemRepository StockOrderItemRepository =>
        _stockOrderItemRepository ??= new StockOrderItemRepository(UowDbContext);
    
    private IStockOrderRepository? _stockOrderRepository;
    public IStockOrderRepository StockOrderRepository =>
        _stockOrderRepository ??= new StockOrderRepository(UowDbContext);
    
    private ISupplierRepository? _supplierRepository;
    public ISupplierRepository SupplierRepository =>
        _supplierRepository ??= new SupplierRepository(UowDbContext);
    
    private IWarehouseRepository? _warehouseRepository;
    public IWarehouseRepository WarehouseRepository =>
        _warehouseRepository ??= new WarehouseRepository(UowDbContext);
    
    private IRefreshTokenRepository? _refreshTokenRepository;
    public IRefreshTokenRepository RefreshTokenRepository =>
        _refreshTokenRepository ??= new RefreshTokenRepository(UowDbContext);
}