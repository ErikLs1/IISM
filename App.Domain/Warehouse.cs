using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Warehouse : BaseEntity
{
    [MaxLength(100)]
    public string WarehouseAddress { get; set; } = default!;
    
    [MaxLength(100)]
    public string WarehouseStreet { get; set; } = default!;
    
    [MaxLength(50)]
    public string WarehouseCity { get; set; } = default!;
    
    [MaxLength(50)]
    public string WarehouseState { get; set; } = default!;
    
    [MaxLength(50)]
    public string WarehouseCountry { get; set; } = default!;
    
    [MaxLength(20)]
    public string WarehousePostalCode { get; set; } = default!;
    
    [MaxLength(100)]
    public string WarehouseEmail { get; set; } = default!;
    
    public int WarehouseCapacity { get; set; }

    public ICollection<StockOrder>? StockOrders { get; set; } = new List<StockOrder>();
    public ICollection<Inventory>? Inventories { get; set; }= new List<Inventory>();

}