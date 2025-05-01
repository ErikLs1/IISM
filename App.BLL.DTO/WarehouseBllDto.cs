using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Contracts;

namespace App.BLL.DTO;

public class WarehouseBllDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    [Display(Name = nameof(WarehouseAddress), Prompt = nameof(WarehouseAddress), ResourceType = typeof(Warehouse))]
    public string WarehouseAddress { get; set; } = default!;
    
    [MaxLength(100)]
    [Display(Name = nameof(WarehouseEmail), Prompt = nameof(WarehouseEmail), ResourceType = typeof(Warehouse))]
    public string WarehouseEmail { get; set; } = default!;
    
    [Display(Name = nameof(WarehouseCapacity), Prompt = nameof(WarehouseCapacity), ResourceType = typeof(Warehouse))]
    public int WarehouseCapacity { get; set; }

    public ICollection<StockOrderBllDto>? StockOrders { get; set; } = new List<StockOrderBllDto>();
    public ICollection<InventoryBllDto>? Inventories { get; set; }= new List<InventoryBllDto>();
}