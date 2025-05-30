using Base.Contracts;

namespace App.DAL.DTO;

public class WarehouseDalDto : IDomainId
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
    public ICollection<StockOrderDalDto>? StockOrders { get; set; } = new List<StockOrderDalDto>();
    public ICollection<InventoryDalDto>? Inventories { get; set; }= new List<InventoryDalDto>();
}