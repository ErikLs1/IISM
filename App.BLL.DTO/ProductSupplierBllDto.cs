using Base.Contracts;

namespace App.BLL.DTO;

public class ProductSupplierBllDto : IDomainId
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; } 
    public string ProductName { get; set; } = default!;
    public string ProductDescription { get; set; } = default!;
    public decimal ProductPrice { get; set; }
    public string ProductStatus { get; set; } = default!; 
    public CategoryBllDto? Category { get; set; }
    public ICollection<ProductSupplierBllDto>? ProductSuppliers { get; set; } = new List<ProductSupplierBllDto>();
    public ICollection<OrderProductBllDto>? OrderProducts { get; set; } = new List<OrderProductBllDto>();
    public ICollection<InventoryBllDto>? Inventories { get; set; } = new List<InventoryBllDto>();
    public ICollection<StockOrderItemBllDto>? StockOrderItems { get; set; } = new List<StockOrderItemBllDto>();
}