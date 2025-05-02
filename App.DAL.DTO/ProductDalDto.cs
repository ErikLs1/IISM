using Base.Contracts;

namespace App.DAL.DTO;

public class ProductDalDto : IDomainId
{
    public Guid Id { get; set; } 
    public Guid CategoryId { get; set; } 
    public string ProductName { get; set; } = default!;
    public string ProductDescription { get; set; } = default!;
    public decimal ProductPrice { get; set; }
    public string ProductStatus { get; set; } = default!; 
    public CategoryDalDto? Category { get; set; }
    public ICollection<ProductSupplierDalDto>? ProductSuppliers { get; set; } = new List<ProductSupplierDalDto>();
    public ICollection<OrderProductDalDto>? OrderProducts { get; set; } = new List<OrderProductDalDto>();
    public ICollection<InventoryDalDto>? Inventories { get; set; } = new List<InventoryDalDto>();
    public ICollection<StockOrderItemDalDto>? StockOrderItems { get; set; } = new List<StockOrderItemDalDto>();
}