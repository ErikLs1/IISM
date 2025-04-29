using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class WarehouseDto : IDomainId
{
    public Guid Id { get; set; }

    [MaxLength(100)]
    [Display(Name = nameof(WarehouseAddress), Prompt = nameof(WarehouseAddress), ResourceType = typeof(App.Resources.Domain.Warehouse))]
    public string WarehouseAddress { get; set; } = default!;
    
    [MaxLength(100)]
    [Display(Name = nameof(WarehouseEmail), Prompt = nameof(WarehouseEmail), ResourceType = typeof(App.Resources.Domain.Warehouse))]
    public string WarehouseEmail { get; set; } = default!;
    
    [Display(Name = nameof(WarehouseCapacity), Prompt = nameof(WarehouseCapacity), ResourceType = typeof(App.Resources.Domain.Warehouse))]
    public int WarehouseCapacity { get; set; }

    public ICollection<StockOrder> StockOrders { get; set; } = new List<StockOrder>();
    public ICollection<Inventory> Inventories { get; set; }= new List<Inventory>();

}