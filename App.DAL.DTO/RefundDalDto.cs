using Base.Contracts;

namespace App.DAL.DTO;

public class RefundDalDto : IDomainId
{
    public Guid Id { get; set; } 
    public Guid OrderProductId { get; set; }
    public decimal RefundAmount { get; set; } 
    public string RefundReason { get; set; } = default!; 
    public string RefundStatus { get; set; } = default!;
    public OrderProductDalDto? OrderProduct { get; set; }
}