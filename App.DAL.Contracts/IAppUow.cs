using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUow : IBaseUow
{
    ICategoryRepository CategoryRepository { get; }
    IInventoryRepository InventoryRepository { get; }
    IOrderProductRepository OrderProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    IPersonRepository PersonRepository { get; }
    IProductRepository ProductRepository { get; }
    IProductSupplierRepository ProductSupplierRepository { get; }
    IRefundRepository RefundRepository { get; }
    IStockOrderItemRepository StockOrderItemRepository { get; }
    IStockOrderRepository StockOrderRepository { get; }
    ISupplierRepository SupplierRepository { get; }
    IWarehouseRepository WarehouseRepository { get; }
}