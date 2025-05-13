using Base.Contracts;

namespace App.DTO.V1.DTO;

public class ProductSupplierDto : IDomainId
{
    public Guid Id { get; set; } 
    public Guid SupplierId { get; set; }
    public Guid ProductId { get; set; }
    public decimal UnitCost { get; set; }
    public SupplierDto? Supplier { get; set; }
    public ProductDto? Product { get; set; }
    
}