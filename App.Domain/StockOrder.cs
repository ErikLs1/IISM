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
    
    [Display(Name = nameof(TotalCost), Prompt = nameof(TotalCost), ResourceType = typeof(App.Resources.Domain.StockOrder))]
    public decimal TotalCost { get; set; }
    
    [Required] 
    [MaxLength(50)] 
    [Display(Name = nameof(Status), Prompt = nameof(Status), ResourceType = typeof(App.Resources.Domain.StockOrder))]
    public string Status { get; set; } = default!;
    
    public Supplier? Supplier { get; set; }
    
    public Warehouse? Warehouse { get; set; }

    public ICollection<StockOrderItem>? StockOrderItems { get; set; } = new List<StockOrderItem>();

}