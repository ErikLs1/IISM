namespace WebApp.Models.Index.MvcDto;

public class PaymentMvcDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; } 
    public string PaymentMethod { get; set; } = default!;
    public string PaymentStatus { get; set; } = default!;
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

}