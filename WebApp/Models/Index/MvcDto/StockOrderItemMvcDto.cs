using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

public class StockOrderItemMvcDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid StockOrderId { get; set; }
    
    [Display(Name = nameof(StockOrderItem.Quantity), ResourceType = typeof(StockOrderItem))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(StockOrderItem.Cost), ResourceType = typeof(StockOrderItem))]
    public decimal Cost { get; set; }
    
    [Display(Name = nameof(StockOrderItem.ProductName), ResourceType = typeof(StockOrderItem))]
    public string? ProductName { get; set; }
}