namespace WebApp.Models.Index.MvcDto;

/// <summary>
/// 
/// </summary>
public class OrderMvcDto
{
    public Guid Id { get; set; }
    public string OrderShippingAddress { get; set; } = default!;
    public string OrderStatus { get; set; } = default!;
    public decimal OrderTotalPrice { get; set; }
}