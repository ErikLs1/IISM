using Base.Contracts;

namespace App.DAL.DTO;

public class OrderDalDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string OrderShippingAddress { get; set; } = default!;
    public string OrderStatus { get; set; } = default!;
    public decimal OrderTotalPrice { get; set; }
    public PersonDalDto? Person { get; set; }
    public ICollection<OrderProductDalDto>? OrderProducts { get; set; } = new List<OrderProductDalDto>();
    public ICollection<PaymentDalDto>? Payments { get; set; } = new List<PaymentDalDto>();
}