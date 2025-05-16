namespace App.DTO.V1.DTO;

public class PlacedOrderDto
{
    public Guid OrderId { get; set; }
    public string CustomerFirstName { get; set; } = default!;
    public string CustomerLastName { get; set; } = default!;
    public int TotalNumberOfProducts { get; set; }
    public DateTime OrderedAt { get; set; }
    public string OrderStatus { get; set; } = default!;
    public IEnumerable<OrderProductDto> Products { get; set; } = default!;
}