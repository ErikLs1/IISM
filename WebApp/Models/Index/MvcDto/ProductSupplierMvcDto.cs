namespace WebApp.Models.Index.MvcDto;

public class ProductSupplierMvcDto
{
    public Guid Id { get; set; }
    public decimal UnitCost { get; set; }
    public string SupplierName { get; set; } = default!;
    public string ProductName { get; set; } = default!;
}