namespace WebApp.Models.Index.MvcDto;

public class StockOrderMvcDto
{
    public Guid Id { get; set; }
    public decimal TotalCost { get; set; }
    public string Status { get; set; } = default!;
    public string SupplierName { get; set; } = default!;
    public string WarehouseAdddress { get; set; } = default!;
}