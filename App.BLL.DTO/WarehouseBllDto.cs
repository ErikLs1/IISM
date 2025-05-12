using Base.Contracts;

namespace App.BLL.DTO;

public class WarehouseBllDto : IDomainId
{
    public Guid Id { get; set; }
    public string WarehouseAddress { get; set; } = default!;
    public string WarehouseStreet { get; set; } = default!;
    public string WarehouseCity { get; set; } = default!;
    public string WarehouseState { get; set; } = default!;
    public string WarehouseCountry { get; set; } = default!;
    public string WarehousePostalCode { get; set; } = default!;
    public string WarehouseEmail { get; set; } = default!;
    public int WarehouseCapacity { get; set; }
    public ICollection<StockOrderBllDto>? StockOrders { get; set; } = new List<StockOrderBllDto>();
    public ICollection<InventoryBllDto>? Inventories { get; set; }= new List<InventoryBllDto>();
}