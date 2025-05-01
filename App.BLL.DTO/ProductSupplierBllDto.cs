using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.BLL.DTO;

public class ProductSupplierBllDto : IDomainId
{
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; set; }

    [MaxLength(100)]
    [Display(Name = nameof(ProductName), Prompt = nameof(ProductName), ResourceType = typeof(Product))]
    public string ProductName { get; set; } = default!;
    
    [MaxLength(250)]
    [Display(Name = nameof(ProductDescription), Prompt = nameof(ProductDescription), ResourceType = typeof(Product))]
    public string ProductDescription { get; set; } = default!;
    
    [Display(Name = nameof(ProductPrice), Prompt = nameof(ProductPrice), ResourceType = typeof(Product))]
    public decimal ProductPrice { get; set; }
    
    [MaxLength(50)]
    [Display(Name = nameof(ProductStatus), Prompt = nameof(ProductStatus), ResourceType = typeof(Product))]
    public string ProductStatus { get; set; } = default!;
  
    [Display(Name = nameof(Category), Prompt = nameof(Category), ResourceType = typeof(Product))]
    public CategoryBllDto? Category { get; set; }
    public ICollection<ProductSupplierBllDto>? ProductSuppliers { get; set; } = new List<ProductSupplierBllDto>();
    public ICollection<OrderProductBllDto>? OrderProducts { get; set; } = new List<OrderProductBllDto>();
    public ICollection<InventoryBllDto>? Inventories { get; set; } = new List<InventoryBllDto>();
    public ICollection<StockOrderItemBllDto>? StockOrderItems { get; set; } = new List<StockOrderItemBllDto>();
}