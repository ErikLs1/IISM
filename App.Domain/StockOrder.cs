using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class StockOrder : BaseEntity
{
    [ForeignKey(nameof(Supplier))]
    public Guid SupplierId { get; set; }
    
    [ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; set; }
    public decimal TotalCost { get; set; }
    
    [Required] 
    [MaxLength(50)] 
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public Supplier? Supplier { get; set; }
    public Warehouse? Warehouse { get; set; }

    public ICollection<StockOrderItem> StockOrderItems { get; set; } = new List<StockOrderItem>();

}