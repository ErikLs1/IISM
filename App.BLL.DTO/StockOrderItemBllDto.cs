using Base.Contracts;

namespace App.BLL.DTO;

public class StockOrderItemBllDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid StockOrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public StockOrderBllDto? StockOrder { get; set; }
    public ProductBllDto? Product { get; set; }
}