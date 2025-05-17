using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Inventory : BaseEntity
{
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    
    [ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; set; }
    
    public int Quantity { get; set; }
    
    public Product? Product { get; set; }
    
    public Warehouse? Warehouse { get; set; }
}