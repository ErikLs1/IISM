using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class RefundDalDto : IDomainId
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(OrderProduct))]
    public Guid OrderProductId { get; set; }
    
    [Display(Name = nameof(RefundAmount), Prompt = nameof(RefundAmount), ResourceType = typeof(Refund))]
    public decimal RefundAmount { get; set; }

    [MaxLength(200)]
    [Display(Name = nameof(RefundReason), Prompt = nameof(RefundReason), ResourceType = typeof(Refund))]
    public string RefundReason { get; set; } = default!;

    [MaxLength(50)]
    [Display(Name = nameof(RefundStatus), Prompt = nameof(RefundStatus), ResourceType = typeof(Refund))]
    public string RefundStatus { get; set; } = default!;
    
    [Display(Name = nameof(OrderProduct), Prompt = nameof(OrderProduct), ResourceType = typeof(Refund))]
    public OrderProductDalDto? OrderProduct { get; set; }

}