namespace App.BLL.DTO;

public class PlacedOrderBllDto
{
    public Guid OrderId { get; set; }
    public string CustomerFirstName { get; set; } = default!;
    public string CustomerLastName { get; set; } = default!;
    public int TotalNumberOfProducts { get; set; }
    public DateTime OrderedAt { get; set; }
    public string OrderStatus { get; set; } = default!;
    public IEnumerable<OrderProductBllDto> Products { get; set; } = default!;
}