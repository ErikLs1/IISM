using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class OrderProduct : BaseEntity
{
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    
    [Display(Name = nameof(Quantity), Prompt = nameof(Quantity), ResourceType = typeof(App.Resources.Domain.OrderProduct))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(TotalPrice), Prompt = nameof(TotalPrice), ResourceType = typeof(App.Resources.Domain.OrderProduct))]
    public decimal TotalPrice { get; set; }
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = nameof(UpdatedAt), Prompt = nameof(UpdatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(App.Resources.Domain.OrderProduct))]
    public Product? Product { get; set; }
    
    [Display(Name = nameof(Order), Prompt = nameof(Order), ResourceType = typeof(App.Resources.Domain.OrderProduct))]
    public Order? Order { get; set; }
    public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}