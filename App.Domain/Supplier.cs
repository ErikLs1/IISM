using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Supplier : BaseEntity
{
    [MaxLength(100)]
    public string SupplierName { get; set; } = default!;
    
    [MaxLength(20)]
    public string SupplierPhoneNumber { get; set; } = default!;
    
    [MaxLength(100)]
    public string SupplierEmail { get; set; } = default!;
    
    [MaxLength(200)]
    public string SupplierAddress { get; set; } = default!;

    [MaxLength(100)]
    public string SupplierStreet { get; set; } = default!;
    
    [MaxLength(50)]
    public string SupplierCity { get; set; } = default!;
    
    [MaxLength(50)]
    public string SupplierState { get; set; } = default!;
    
    [MaxLength(50)]
    public string SupplierCountry { get; set; } = default!;
    
    [MaxLength(20)]
    public string SupplierPostalCode { get; set; } = default!;

    public ICollection<StockOrder>? StockOrders { get; set; } = new List<StockOrder>();
    public ICollection<ProductSupplier>? ProductSuppliers { get; set; } = new List<ProductSupplier>();

}