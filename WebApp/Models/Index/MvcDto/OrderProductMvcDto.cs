using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

public class OrderProductMvcDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    
    [Display(Name = nameof(OrderProduct.Quantity), ResourceType = typeof(OrderProduct))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(OrderProduct.TotalPrice), ResourceType = typeof(OrderProduct))]
    public decimal TotalPrice { get; set; }
    
    [Display(Name = nameof(OrderProduct.ProductName), ResourceType = typeof(OrderProduct))]
    public string? ProductName { get; set; }
}