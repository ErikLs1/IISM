using Base.Contracts;

namespace App.BLL.DTO;

public class SupplierBllDto : IDomainId
{
    public Guid Id { get; set; }
    public string SupplierName { get; set; } = default!;
    public string SupplierPhoneNumber { get; set; } = default!;
    public string SupplierEmail { get; set; } = default!;
    public string SupplierAddress { get; set; } = default!;
    public string SupplierStreet { get; set; } = default!;
    public string SupplierCity { get; set; } = default!;
    public string SupplierState { get; set; } = default!;
    public string SupplierCountry { get; set; } = default!;
    public string SupplierPostalCode { get; set; } = default!;
    public ICollection<StockOrderBllDto>? StockOrders { get; set; } = new List<StockOrderBllDto>();
    public ICollection<ProductSupplierBllDto>? ProductSuppliers { get; set; } = new List<ProductSupplierBllDto>();
}