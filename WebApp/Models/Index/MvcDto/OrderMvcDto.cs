using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

/// <summary>
/// 
/// </summary>
public class OrderMvcDto
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Order.OrderShippingAddress), ResourceType = typeof(Order))]
    public string OrderShippingAddress { get; set; } = default!;
    
    [Display(Name = nameof(Order.OrderStatus), ResourceType = typeof(Order))]
    public string OrderStatus { get; set; } = default!;
    
    [Display(Name = nameof(Order.OrderTotalPrice), ResourceType = typeof(Order))]
    public decimal OrderTotalPrice { get; set; }
}