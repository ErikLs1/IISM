using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class ProductSupplierDalDto : IDomainId
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(Supplier))]
    public Guid SupplierId { get; set; }
    
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [Display(Name = nameof(UnitCost), Prompt = nameof(UnitCost), ResourceType = typeof(ProductSupplier))]
    public decimal UnitCost { get; set; }
    
    [Display(Name = nameof(Supplier), Prompt = nameof(Supplier), ResourceType = typeof(ProductSupplier))]
    public SupplierDalDto? Supplier { get; set; }
    
    [Display(Name = nameof(Product), Prompt = nameof(Product), ResourceType = typeof(ProductSupplier))]
    public ProductDalDto? Product { get; set; }
}