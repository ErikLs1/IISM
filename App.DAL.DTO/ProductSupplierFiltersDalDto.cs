namespace App.DAL.DTO;

public class ProductSupplierFiltersDalDto
{
    public IEnumerable<string> Cities { get; set; } = Array.Empty<string>();
    public IEnumerable<string> States { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Countries { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Categories { get; set; } = Array.Empty<string>(); // Product Category
    public IEnumerable<string> Suppliers { get; set; } = Array.Empty<string>(); // Supplier name
}