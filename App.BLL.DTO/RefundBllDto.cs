using Base.Contracts;

namespace App.BLL.DTO;

public class RefundBllDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid OrderProductId { get; set; }
    public decimal RefundAmount { get; set; } 
    public string RefundReason { get; set; } = default!; 
    public string RefundStatus { get; set; } = default!;
    public OrderProductBllDto? OrderProduct { get; set; }
}