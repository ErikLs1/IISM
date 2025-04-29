using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class InventoryDto : IDomainId
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; set; }
    
    [Display(Name = nameof(Quantity), Prompt = nameof(Quantity), ResourceType = typeof(App.Resources.Domain.Inventory))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(App.Resources.Domain.Inventory))]
    public Product? Product { get; set; }
    
    [Display(Name = nameof(Warehouse), Prompt = nameof(Warehouse), ResourceType = typeof(App.Resources.Domain.Inventory))]
    public Warehouse? Warehouse { get; set; }

}