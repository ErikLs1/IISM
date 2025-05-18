using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

public class PaymentMvcDto
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Payment.OrderId), ResourceType = typeof(Payment))]
    public Guid OrderId { get; set; } 
    
    [Display(Name = nameof(Payment.PaymentMethod), ResourceType = typeof(Payment))]
    public string PaymentMethod { get; set; } = default!;
    
    [Display(Name = nameof(Payment.PaymentStatus), ResourceType = typeof(Payment))]
    public string PaymentStatus { get; set; } = default!;
    
    [Display(Name = nameof(Payment.PaymentAmount), ResourceType = typeof(Payment))]
    public decimal PaymentAmount { get; set; }
    
    [Display(Name = nameof(Payment.PaymentDate), ResourceType = typeof(Payment))]
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    
    public string? ShippingAddress { get; set; }
}