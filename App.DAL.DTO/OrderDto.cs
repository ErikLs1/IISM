using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class OrderDto : IDomainId
{
    public Guid Id { get; set; }
    
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
    
    [Display(Name = nameof(Person), Prompt = nameof(Person), ResourceType = typeof(App.Resources.Domain.Order))]
    public PersonDto? Person { get; set; }
    public ICollection<OrderProductDto>? OrderProducts { get; set; } = new List<OrderProductDto>();
    public ICollection<PaymentDto>? Payments { get; set; } = new List<PaymentDto>();
}