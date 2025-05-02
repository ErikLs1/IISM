using Base.Contracts;

namespace App.DAL.DTO;

public class PaymentDalDto :IDomainId
{
    public Guid Id { get; set; } 
    public Guid OrderId { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string PaymentStatus { get; set; } = default!;
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public OrderDalDto? Order { get; set; }
}