using Base.Contracts;

namespace App.DAL.DTO;

public class InventoryDalDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public ProductDalDto? Product { get; set; }
    public WarehouseDalDto? Warehouse { get; set; }
}