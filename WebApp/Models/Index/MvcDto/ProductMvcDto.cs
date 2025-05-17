namespace WebApp.Models.Index.MvcDto;

public class ProductMvcDto
{
    public Guid Id { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public string ProductDescription { get; set; } = default!;
    public decimal ProductPrice { get; set; }
}