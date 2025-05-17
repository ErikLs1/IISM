using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

public class ProductMvcDto
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Product.ProductName), ResourceType = typeof(Product))]
    public string ProductName { get; set; } = default!;
    
    [Display(Name = nameof(Product.ProductDescription), ResourceType = typeof(Product))]
    public string ProductDescription { get; set; } = default!;
    
    [Display(Name = nameof(Product.ProductPrice), ResourceType = typeof(Product))]
    public decimal ProductPrice { get; set; }
}