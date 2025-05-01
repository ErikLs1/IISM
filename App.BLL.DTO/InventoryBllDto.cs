using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.BLL.DTO;

public class InventoryBllDto : IDomainId
{
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; set; }
    
    [Display(Name = nameof(Quantity), Prompt = nameof(Quantity), ResourceType = typeof(Inventory))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(Inventory))]
    public ProductBllDto? Product { get; set; }
    
    [Display(Name = nameof(Warehouse), Prompt = nameof(Warehouse), ResourceType = typeof(Inventory))]
    public WarehouseBllDto? Warehouse { get; set; }
}