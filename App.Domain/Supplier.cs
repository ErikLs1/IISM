using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Supplier : BaseEntity
{
    [MaxLength(100)]
    [Display(Name = nameof(SupplierName), Prompt = nameof(SupplierName), ResourceType = typeof(App.Resources.Domain.Supplier))]
    public string SupplierName { get; set; } = default!;
    
    [MaxLength(20)]
    [Display(Name = nameof(SupplierPhoneNumber), Prompt = nameof(SupplierPhoneNumber), ResourceType = typeof(App.Resources.Domain.Supplier))]
    public string SupplierPhoneNumber { get; set; } = default!;
    
    [MaxLength(100)]
    [Display(Name = nameof(SupplierEmail), Prompt = nameof(SupplierEmail), ResourceType = typeof(App.Resources.Domain.Supplier))]
    public string SupplierEmail { get; set; } = default!;
    
    [MaxLength(200)]
    [Display(Name = nameof(SupplierAddress), Prompt = nameof(SupplierAddress), ResourceType = typeof(App.Resources.Domain.Supplier))]
    public string SupplierAddress { get; set; } = default!;
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = nameof(UpdatedAt), Prompt = nameof(UpdatedAt), ResourceType = typeof(Base.Resources.Common))]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<StockOrder> StockOrders { get; set; } = new List<StockOrder>();
    public ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();

}