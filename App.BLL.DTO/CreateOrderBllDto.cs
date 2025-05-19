namespace App.BLL.DTO;

public class CreateOrderBllDto
{
    public string ShippingAddress { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
    public IEnumerable<CreateOrderProductBllDto> Products { get; set; } = default!;
}