namespace WebApp.Models.Index.MvcDto;

public class OrderProductMvcDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string ProductName { get; set; } = default!;
}