using Base.Contracts;

namespace App.BLL.DTO;

public class OrderBllDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; } 
    public string OrderShippingAddress { get; set; } = default!;
    public string OrderStatus { get; set; } = default!;
    public decimal OrderTotalPrice { get; set; }
    public PersonBllDto? Person { get; set; }
    public ICollection<OrderProductBllDto>? OrderProducts { get; set; } = new List<OrderProductBllDto>();
    public ICollection<PaymentBllDto>? Payments { get; set; } = new List<PaymentBllDto>();
}