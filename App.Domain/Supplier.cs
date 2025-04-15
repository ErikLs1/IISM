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
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<StockOrder> StockOrders { get; set; } = new List<StockOrder>();
    public ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();

}