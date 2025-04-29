using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class ProductSupplier : BaseEntity
{
    [ForeignKey(nameof(Supplier))]
    public Guid SupplierId { get; set; }
    
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [Display(Name = nameof(UnitCost), Prompt = nameof(UnitCost), ResourceType = typeof(App.Resources.Domain.ProductSupplier))]
    public decimal UnitCost { get; set; }
    
    [Display(Name = nameof(Supplier), Prompt = nameof(Supplier), ResourceType = typeof(App.Resources.Domain.ProductSupplier))]
    public Supplier? Supplier { get; set; }
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(App.Resources.Domain.ProductSupplier))]
    public Product? Product { get; set; }

}