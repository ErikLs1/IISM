using Base.Contracts;

namespace App.BLL.DTO;

public class InventoryBllDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public ProductBllDto? Product { get; set; }
    public WarehouseBllDto? Warehouse { get; set; }
}