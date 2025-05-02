using Base.Contracts;

namespace App.DAL.DTO;

public class ProductSupplierDalDto : IDomainId
{
    public Guid Id { get; set; } 
    public Guid SupplierId { get; set; }
    public Guid ProductId { get; set; }
    public decimal UnitCost { get; set; }
    public SupplierDalDto? Supplier { get; set; }
    public ProductDalDto? Product { get; set; }
}