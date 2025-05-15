namespace App.DTO.V1.DTO;

public class UserOrdersDto
{
    public decimal OrderTotalPrice { get; set; }
    public string OrderShippingAddress { get; set; } = default!;
    public string OrderStatus { get; set; } = default!;
    public IEnumerable<OrderProductDto> Products { get; set; } = default!;
}