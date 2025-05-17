using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class StockOrderItem : BaseEntity
{
    [ForeignKey(nameof(StockOrder))]
    public Guid StockOrderId { get; set; }
    
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [Display(Name = nameof(Quantity), Prompt = nameof(Quantity), ResourceType = typeof(App.Resources.Domain.StockOrderItem))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(Cost), Prompt = nameof(Cost), ResourceType = typeof(App.Resources.Domain.StockOrderItem))]
    public decimal Cost { get; set; }
    
    public StockOrder? StockOrder { get; set; }
    
    public Product? Product { get; set; }

}