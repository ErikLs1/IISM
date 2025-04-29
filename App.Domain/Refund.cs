using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Refund : BaseEntity
{
    [ForeignKey(nameof(OrderProduct))]
    public Guid OrderProductId { get; set; }
    
    [Display(Name = nameof(RefundAmount), Prompt = nameof(RefundAmount), ResourceType = typeof(App.Resources.Domain.Refund))]
    public decimal RefundAmount { get; set; }

    [MaxLength(200)]
    [Display(Name = nameof(RefundReason), Prompt = nameof(RefundReason), ResourceType = typeof(App.Resources.Domain.Refund))]
    public string RefundReason { get; set; } = default!;

    [MaxLength(50)]
    [Display(Name = nameof(RefundStatus), Prompt = nameof(RefundStatus), ResourceType = typeof(App.Resources.Domain.Refund))]
    public string RefundStatus { get; set; } = default!;
    
    [Display(Name = nameof(OrderProduct), Prompt = nameof(OrderProduct), ResourceType = typeof(App.Resources.Domain.Refund))]
    public OrderProduct? OrderProduct { get; set; }

}