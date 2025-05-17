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
    
    
    public decimal UnitCost { get; set; }
    
    public Supplier? Supplier { get; set; }
    
    public Product? Product { get; set; }

}