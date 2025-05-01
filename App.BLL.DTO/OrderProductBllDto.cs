using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.BLL.DTO;

public class OrderProductBllDto : IDomainId
{
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    
    [Display(Name = nameof(Quantity), Prompt = nameof(Quantity), ResourceType = typeof(OrderProduct))]
    public int Quantity { get; set; }
    
    [Display(Name = nameof(TotalPrice), Prompt = nameof(TotalPrice), ResourceType = typeof(OrderProduct))]
    public decimal TotalPrice { get; set; }
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(OrderProduct))]
    public ProductBllDto? Product { get; set; }
    
    [Display(Name = nameof(Order), Prompt = nameof(Order), ResourceType = typeof(OrderProduct))]
    public OrderBllDto? Order { get; set; }
    public ICollection<RefundBllDto>? Refunds { get; set; } = new List<RefundBllDto>();
}