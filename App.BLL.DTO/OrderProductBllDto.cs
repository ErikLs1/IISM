using Base.Contracts;

namespace App.BLL.DTO;

public class OrderProductBllDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal OrderProductPrice { get; set; }
    public string ProductName { get; set; } = default!;
    public string ProductDescription { get; set; } = default!;
    
    public ProductBllDto? Product { get; set; }
    public OrderBllDto? Order { get; set; }
    public ICollection<RefundBllDto>? Refunds { get; set; } = new List<RefundBllDto>();
}