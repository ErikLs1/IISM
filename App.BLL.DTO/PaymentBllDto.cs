using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.BLL.DTO;

public class PaymentBllDto : IDomainId
{
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    
    [MaxLength(50)] 
    [Display(Name = nameof(PaymentStatus), Prompt = nameof(PaymentStatus), ResourceType = typeof(Payment))]
    public string PaymentMethod { get; set; } = default!;
    
    [MaxLength(50)] 
    [Display(Name = nameof(PaymentStatus), Prompt = nameof(PaymentStatus), ResourceType = typeof(Payment))]
    public string PaymentStatus { get; set; } = default!;
    
    [Display(Name = nameof(PaymentAmount), Prompt = nameof(PaymentAmount), ResourceType = typeof(Payment))]
    public decimal PaymentAmount { get; set; }
    
    [Display(Name = nameof(PaymentDate), Prompt = nameof(PaymentDate), ResourceType = typeof(Payment))]
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    
    [Display(Name = nameof(Order), Prompt = nameof(Order), ResourceType = typeof(Payment))]
    public OrderBllDto? Order { get; set; }
}