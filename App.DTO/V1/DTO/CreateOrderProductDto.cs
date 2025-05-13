namespace App.DTO.V1.DTO;

public class CreateOrderProductDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}