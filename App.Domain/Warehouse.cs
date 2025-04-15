using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Warehouse : BaseEntity
{
    [MaxLength(100)]
    public string WarehouseAddress { get; set; } = default!;
    
    [MaxLength(100)]
    public string WarehouseEmail { get; set; } = default!;
    public int WarehouseCapacity { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<StockOrder> StockOrders { get; set; } = new List<StockOrder>();
    public ICollection<Inventory> Inventories { get; set; }= new List<Inventory>();

}