using Base.Contracts;

namespace App.BLL.DTO;

public class ProductSupplierBllDto : IDomainId
{
   public Guid Id { get; set; } 
   public Guid SupplierId { get; set; }
   public Guid ProductId { get; set; }
   public decimal UnitCost { get; set; }
   public SupplierBllDto? Supplier { get; set; }
   public ProductBllDto? Product { get; set; }
}