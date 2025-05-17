using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

/// <summary>
/// 
/// </summary>
public class RefundMvcDto
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Refund.RefundAmount), ResourceType = typeof(Refund))]
    public decimal RefundAmount { get; set; } 
    
    [Display(Name = nameof(Refund.RefundReason), ResourceType = typeof(Refund))]
    public string RefundReason { get; set; } = default!;
    
    [Display(Name = nameof(Refund.RefundStatus), ResourceType = typeof(Refund))]
    public string RefundStatus { get; set; } = default!;
}