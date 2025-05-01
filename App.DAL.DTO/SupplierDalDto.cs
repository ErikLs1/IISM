using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class SupplierDalDto : IDomainId
{
    public Guid Id { get; set; }

    [MaxLength(100)]
    [Display(Name = nameof(SupplierName), Prompt = nameof(SupplierName), ResourceType = typeof(Supplier))]
    public string SupplierName { get; set; } = default!;
    
    [MaxLength(20)]
    [Display(Name = nameof(SupplierPhoneNumber), Prompt = nameof(SupplierPhoneNumber), ResourceType = typeof(Supplier))]
    public string SupplierPhoneNumber { get; set; } = default!;
    
    [MaxLength(100)]
    [Display(Name = nameof(SupplierEmail), Prompt = nameof(SupplierEmail), ResourceType = typeof(Supplier))]
    public string SupplierEmail { get; set; } = default!;
    
    [MaxLength(200)]
    [Display(Name = nameof(SupplierAddress), Prompt = nameof(SupplierAddress), ResourceType = typeof(Supplier))]
    public string SupplierAddress { get; set; } = default!;

    public ICollection<StockOrderDalDto>? StockOrders { get; set; } = new List<StockOrderDalDto>();
    public ICollection<ProductSupplierDalDto>? ProductSuppliers { get; set; } = new List<ProductSupplierDalDto>();
}