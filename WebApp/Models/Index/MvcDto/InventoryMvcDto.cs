using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;


public class InventoryMvcDto
{

    public Guid Id { get; set; }
    
    [Display(Name = nameof(Inventory.Quantity), ResourceType = typeof(Inventory))]
    
    public int Quantity { get; set; }
    
    [Display(Name = nameof(Inventory.ProductName), ResourceType = typeof(Inventory))]
    public string? ProductName { get; set; }
    
    [Display(Name = nameof(Inventory.ProductDescription), ResourceType = typeof(Inventory))]
    public string? ProductDescription { get; set; }
    
    [Display(Name = nameof(Inventory.WarehouseAddress), ResourceType = typeof(Inventory))]
    public string? WarehouseAddress { get; set; }
}