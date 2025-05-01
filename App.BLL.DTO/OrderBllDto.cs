using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.BLL.DTO;

public class OrderBllDto : IDomainId
{
    public Guid Id { get; set; }
    [ForeignKey(nameof(Person))]
    public Guid PersonId { get; set; }

    [MaxLength(200)]
    [Display(Name = nameof(OrderShippingAddress), Prompt = nameof(OrderShippingAddress), ResourceType = typeof(Order))]
    public string OrderShippingAddress { get; set; } = default!;
    
    [MaxLength(50)]
    [Display(Name = nameof(OrderStatus), Prompt = nameof(OrderStatus), ResourceType = typeof(Order))]
    public string OrderStatus { get; set; } = default!;
    
    [Display(Name = nameof(OrderTotalPrice), Prompt = nameof(OrderTotalPrice), ResourceType = typeof(Order))]
    public decimal OrderTotalPrice { get; set; }
    
    [Display(Name = nameof(Person), Prompt = nameof(Person), ResourceType = typeof(Order))]
    public PersonBllDto? Person { get; set; }
    public ICollection<OrderProductBllDto>? OrderProducts { get; set; } = new List<OrderProductBllDto>();
    public ICollection<PaymentBllDto>? Payments { get; set; } = new List<PaymentBllDto>();
}