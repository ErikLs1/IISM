using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class StockOrderItem : BaseEntity
{
    [ForeignKey(nameof(StockOrder))]
    public Guid StockOrderId { get; set; }
    
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public StockOrder? StockOrder { get; set; }
    public Product? Product { get; set; }

}