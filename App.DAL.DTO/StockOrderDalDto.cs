using Base.Contracts;

namespace App.DAL.DTO;

public class StockOrderDalDto : IDomainId
{
    public Guid Id { get; set; } 
    public Guid SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    public decimal TotalCost { get; set; }
    public string Status { get; set; } = default!;
    public SupplierDalDto? Supplier { get; set; }
    public WarehouseDalDto? Warehouse { get; set; } 
    public ICollection<StockOrderItemDalDto>? StockOrderItems { get; set; } = new List<StockOrderItemDalDto>();
}