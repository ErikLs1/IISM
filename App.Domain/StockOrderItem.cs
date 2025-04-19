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
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = nameof(UpdatedAt), Prompt = nameof(UpdatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = nameof(StockOrder), Prompt = nameof(StockOrder), ResourceType = typeof(App.Resources.Domain.StockOrderItem))]
    public StockOrder? StockOrder { get; set; }
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(App.Resources.Domain.StockOrderItem))]
    public Product? Product { get; set; }

}