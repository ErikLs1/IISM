using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class ProductDto : IDomainId
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; set; }

    [MaxLength(100)]
    [Display(Name = nameof(ProductName), Prompt = nameof(ProductName), ResourceType = typeof(App.Resources.Domain.Product))]
    public string ProductName { get; set; } = default!;
    
    [MaxLength(250)]
    [Display(Name = nameof(ProductDescription), Prompt = nameof(ProductDescription), ResourceType = typeof(App.Resources.Domain.Product))]
    public string ProductDescription { get; set; } = default!;
    
    [Display(Name = nameof(ProductPrice), Prompt = nameof(ProductPrice), ResourceType = typeof(App.Resources.Domain.Product))]
    public decimal ProductPrice { get; set; }
    
    [MaxLength(50)]
    [Display(Name = nameof(ProductStatus), Prompt = nameof(ProductStatus), ResourceType = typeof(App.Resources.Domain.Product))]
    public string ProductStatus { get; set; } = default!;
  
    [Display(Name = nameof(Category), Prompt = nameof(Category), ResourceType = typeof(App.Resources.Domain.Product))]
    public Category? Category { get; set; }
    public ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    public ICollection<StockOrderItem> StockOrderItems { get; set; } = new List<StockOrderItem>();
}