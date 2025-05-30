using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

public class StockOrderMvcDto
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    
    [Display(Name = nameof(StockOrder.TotalCost), ResourceType = typeof(StockOrder))]
    public decimal TotalCost { get; set; }
    
    [Display(Name = nameof(StockOrder.Status), ResourceType = typeof(StockOrder))]
    public string Status { get; set; } = default!;
    
    public string? SupplierName { get; set; }
    public string? WarehouseAddress { get; set; } 
}