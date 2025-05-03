using Base.Contracts;

namespace App.BLL.DTO;

public class StockOrderBllDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    public decimal TotalCost { get; set; }
    public string Status { get; set; } = default!;
    public SupplierBllDto? Supplier { get; set; }
    public WarehouseBllDto? Warehouse { get; set; } 
    public ICollection<StockOrderItemBllDto>? StockOrderItems { get; set; } = new List<StockOrderItemBllDto>();
}