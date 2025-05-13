namespace App.DTO.V1.DTO;

public class CreateOrderDto
{
    public string ShippingAddress { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
    public IEnumerable<CreateOrderProductDto> Products { get; set; } = default!;
}