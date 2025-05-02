using Base.Contracts;

namespace App.DAL.DTO;

public class StockOrderItemDalDto : IDomainId
{
    public Guid Id { get; set; } 
    public Guid StockOrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public StockOrderDalDto? StockOrder { get; set; }
    public ProductDalDto? Product { get; set; }
}