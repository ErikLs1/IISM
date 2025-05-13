using Base.Contracts;

namespace App.DTO.V1.DTO;

// Created for test
public class StockOrderDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    public decimal TotalCost { get; set; }
    public string Status { get; set; } = default!;
    public SupplierDto? Supplier { get; set; }
    public WarehouseDto? Warehouse { get; set; } 
    public ICollection<StockOrderItemDto>? StockOrderItems { get; set; } = new List<StockOrderItemDto>();
}