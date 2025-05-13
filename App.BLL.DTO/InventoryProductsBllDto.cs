namespace App.BLL.DTO;

public class InventoryProductsBllDto
{
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
    
    public decimal ProductPrice { get; set; }
    public string WarehouseCity { get; set; } = default!;
    public string WarehouseState { get; set; } = default!;
    public string WarehouseCountry { get; set; } = default!;
    public string ProductDescription { get; set; } = default!;
}