namespace App.BLL.DTO;

public class UserOrdersBllDto
{
    public decimal OrderTotalPrice { get; set; }
    public string OrderShippingAddress { get; set; } = default!;
    public string OrderStatus { get; set; } = default!;
    public IEnumerable<OrderProductBllDto> Products { get; set; } = default!;
}