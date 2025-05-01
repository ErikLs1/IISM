using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.BLL.DTO;

public class StockOrderItemBllDto : IDomainId
{
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(StockOrder))]
    public Guid StockOrderId { get; set; }
    
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [Display(Name = nameof(Quantity), Prompt = nameof(Quantity), ResourceType = typeof(StockOrderItem))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(Cost), Prompt = nameof(Cost), ResourceType = typeof(StockOrderItem))]
    public decimal Cost { get; set; }
    
    [Display(Name = nameof(StockOrder), Prompt = nameof(StockOrder), ResourceType = typeof(StockOrderItem))]
    public StockOrderBllDto? StockOrder { get; set; }
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(StockOrderItem))]
    public ProductBllDto? Product { get; set; }
}