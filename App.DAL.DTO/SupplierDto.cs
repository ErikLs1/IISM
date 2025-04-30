using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class SupplierDto : IDomainId
{
    public Guid Id { get; set; }

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

    public ICollection<StockOrderDto>? StockOrders { get; set; } = new List<StockOrderDto>();
    public ICollection<ProductSupplierDto>? ProductSuppliers { get; set; } = new List<ProductSupplierDto>();
}