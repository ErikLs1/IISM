using Base.Contracts;

namespace App.BLL.DTO;

public class PaymentBllDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string PaymentStatus { get; set; } = default!;
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public OrderBllDto? Order { get; set; }
}