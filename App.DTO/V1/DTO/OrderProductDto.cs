namespace App.DTO.V1.DTO;

public class OrderProductDto
{
    public int Quantity { get; set; }
    public decimal OrderProductPrice { get; set; }
    public string ProductName { get; set; } = default!;
    public string ProductDescription { get; set; } = default!;
}