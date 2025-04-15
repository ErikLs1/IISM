using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Payment : BaseEntity
{
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    
    [MaxLength(50)] 
    public string PaymentMethod { get; set; } = default!;
    
    [MaxLength(50)] 
    public string PaymentStatus { get; set; } = default!;
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    
    public Order? Order { get; set; }
}