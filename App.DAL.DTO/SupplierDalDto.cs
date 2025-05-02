using Base.Contracts;

namespace App.DAL.DTO;

public class SupplierDalDto : IDomainId
{
    public Guid Id { get; set; } 
    public string SupplierName { get; set; } = default!;
    public string SupplierPhoneNumber { get; set; } = default!;
    public string SupplierEmail { get; set; } = default!;
    public string SupplierAddress { get; set; } = default!; 
    public ICollection<StockOrderDalDto>? StockOrders { get; set; } = new List<StockOrderDalDto>();
    public ICollection<ProductSupplierDalDto>? ProductSuppliers { get; set; } = new List<ProductSupplierDalDto>();
}