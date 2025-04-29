using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class StockOrderItemDto : IDomainId
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(StockOrder))]
    public Guid StockOrderId { get; set; }
    
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [Display(Name = nameof(Quantity), Prompt = nameof(Quantity), ResourceType = typeof(App.Resources.Domain.StockOrderItem))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(Cost), Prompt = nameof(Cost), ResourceType = typeof(App.Resources.Domain.StockOrderItem))]
    public decimal Cost { get; set; }
    
    [Display(Name = nameof(StockOrder), Prompt = nameof(StockOrder), ResourceType = typeof(App.Resources.Domain.StockOrderItem))]
    public StockOrder? StockOrder { get; set; }
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(App.Resources.Domain.StockOrderItem))]
    public Product? Product { get; set; }
}