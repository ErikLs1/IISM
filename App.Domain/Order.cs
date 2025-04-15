using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Order : BaseEntity
{
    [ForeignKey(nameof(Person))]
    public Guid PersonId { get; set; }

    [MaxLength(200)]
    public string OrderShippingAddress { get; set; } = default!;
    
    [MaxLength(50)]
    public string OrderStatus { get; set; } = default!;
    public decimal OrderTotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Person? Person { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}