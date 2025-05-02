using Base.Contracts;

namespace App.DAL.DTO;

public class OrderProductDalDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
     public ProductDalDto? Product { get; set; }
     public OrderDalDto? Order { get; set; }
    public ICollection<RefundDalDto>? Refunds { get; set; } = new List<RefundDalDto>();
}