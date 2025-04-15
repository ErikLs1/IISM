using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Product : BaseEntity
{
    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; set; }

    [MaxLength(100)]
    public string ProductName { get; set; } = default!;
    
    [MaxLength(250)]
    public string ProductDescription { get; set; } = default!;
    public decimal ProductPrice { get; set; }
    
    [MaxLength(50)]
    public string ProductStatus { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  
    public Category? Category { get; set; }
    public ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    public ICollection<StockOrderItem> StockOrderItems { get; set; } = new List<StockOrderItem>();

}