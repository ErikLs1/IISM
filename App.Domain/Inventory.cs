using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Inventory : BaseEntity
{
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; set; }
    
    [Display(Name = nameof(Quantity), Prompt = nameof(Quantity), ResourceType = typeof(App.Resources.Domain.Inventory))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = nameof(UpdatedAt), Prompt = nameof(UpdatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(App.Resources.Domain.Inventory))]
    public Product? Product { get; set; }
    
    [Display(Name = nameof(Warehouse), Prompt = nameof(Warehouse), ResourceType = typeof(App.Resources.Domain.Inventory))]
    public Warehouse? Warehouse { get; set; }
}