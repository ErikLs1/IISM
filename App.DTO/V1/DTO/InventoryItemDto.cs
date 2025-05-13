namespace App.DTO.V1.DTO;

public class InventoryItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string ProductDescription { get; set; } = default!;
    public int Quantity { get; set; }
}