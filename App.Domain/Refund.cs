using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Refund : BaseEntity
{
    [ForeignKey(nameof(OrderProduct))]
    public Guid OrderProductId { get; set; }
    public decimal RefundAmount { get; set; }

    [MaxLength(200)]
    public string RefundReason { get; set; } = default!;

    [MaxLength(50)]
    public string RefundStatus { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public OrderProduct? OrderProduct { get; set; }

}