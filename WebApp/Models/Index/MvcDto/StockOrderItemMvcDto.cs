namespace WebApp.Models.Index.MvcDto;

public class StockOrderItemMvcDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public string ProductName { get; set; } = default!;
}