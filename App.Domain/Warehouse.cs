using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Warehouse : BaseEntity
{
    [MaxLength(100)]
    [Display(Name = nameof(WarehouseAddress), Prompt = nameof(WarehouseAddress), ResourceType = typeof(App.Resources.Domain.Warehouse))]
    public string WarehouseAddress { get; set; } = default!;
    
    [MaxLength(100)]
    [Display(Name = nameof(WarehouseEmail), Prompt = nameof(WarehouseEmail), ResourceType = typeof(App.Resources.Domain.Warehouse))]
    public string WarehouseEmail { get; set; } = default!;
    
    [Display(Name = nameof(WarehouseCapacity), Prompt = nameof(WarehouseCapacity), ResourceType = typeof(App.Resources.Domain.Warehouse))]
    public int WarehouseCapacity { get; set; }
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = nameof(UpdatedAt), Prompt = nameof(UpdatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<StockOrder> StockOrders { get; set; } = new List<StockOrder>();
    public ICollection<Inventory> Inventories { get; set; }= new List<Inventory>();

}