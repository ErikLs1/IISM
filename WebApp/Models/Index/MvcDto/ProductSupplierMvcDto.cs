using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

public class ProductSupplierMvcDto
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(ProductSupplier.UnitCost), ResourceType = typeof(ProductSupplier))]
    public decimal UnitCost { get; set; }
    
    public Guid SupplierId { get; set; }
    
    [Display(Name = nameof(ProductSupplier.SupplierName), ResourceType = typeof(ProductSupplier))]
    public string? SupplierName { get; set; }
    
    public Guid ProductId { get; set; }
    
    [Display(Name = nameof(ProductSupplier.ProductName), ResourceType = typeof(ProductSupplier))]
    public string? ProductName { get; set; }
}