using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Order : BaseEntity
{
    [ForeignKey(nameof(Person))]
    public Guid PersonId { get; set; }

    [MaxLength(200)]
    [Display(Name = nameof(OrderShippingAddress), Prompt = nameof(OrderShippingAddress), ResourceType = typeof(App.Resources.Domain.Order))]
    public string OrderShippingAddress { get; set; } = default!;
    
    [MaxLength(50)]
    [Display(Name = nameof(OrderStatus), Prompt = nameof(OrderStatus), ResourceType = typeof(App.Resources.Domain.Order))]
    public string OrderStatus { get; set; } = default!;
    
    [Display(Name = nameof(OrderTotalPrice), Prompt = nameof(OrderTotalPrice), ResourceType = typeof(App.Resources.Domain.Order))]
    public decimal OrderTotalPrice { get; set; }
    
    public Person? Person { get; set; }
    public ICollection<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();
    public ICollection<Payment>? Payments { get; set; } = new List<Payment>();
}