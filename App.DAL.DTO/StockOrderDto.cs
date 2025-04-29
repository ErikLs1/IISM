using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class StockOrderDto : IDomainId
{
    public Guid Id { get; set; }

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
    
    
    [Display(Name = nameof(Supplier), Prompt = nameof(Supplier), ResourceType = typeof(App.Resources.Domain.StockOrder))]
    public Supplier? Supplier { get; set; }
    
    [Display(Name = nameof(Warehouse), Prompt = nameof(Warehouse), ResourceType = typeof(App.Resources.Domain.StockOrder))]
    public Warehouse? Warehouse { get; set; }

    public ICollection<StockOrderItem> StockOrderItems { get; set; } = new List<StockOrderItem>();
}