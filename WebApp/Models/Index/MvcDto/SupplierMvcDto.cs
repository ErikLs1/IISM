using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

public class SupplierMvcDto
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Supplier.SupplierName), ResourceType = typeof(Supplier))]
    public string SupplierName { get; set; } = default!;
    
    [Display(Name = nameof(Supplier.SupplierPhoneNumber), ResourceType = typeof(Supplier))]
    public string SupplierPhoneNumber { get; set; } = default!;
    
    [Display(Name = nameof(Supplier.SupplierEmail), ResourceType = typeof(Supplier))]
    public string SupplierEmail { get; set; } = default!;
    
    [Display(Name = nameof(Supplier.SupplierAddress), ResourceType = typeof(Supplier))]
    public string SupplierAddress { get; set; } = default!;
    
    [Display(Name = nameof(Supplier.SupplierStreet), ResourceType = typeof(Supplier))]
    public string SupplierStreet { get; set; } = default!;
    
    [Display(Name = nameof(Supplier.SupplierCity), ResourceType = typeof(Supplier))]
    public string SupplierCity { get; set; } = default!;
    
    [Display(Name = nameof(Supplier.SupplierState), ResourceType = typeof(Supplier))]
    public string SupplierState { get; set; } = default!;
    
    [Display(Name = nameof(Supplier.SupplierCountry), ResourceType = typeof(Supplier))]
    public string SupplierCountry { get; set; } = default!;
    
    [Display(Name = nameof(Supplier.SupplierPostalCode), ResourceType = typeof(Supplier))]
    public string SupplierPostalCode { get; set; } = default!;

}