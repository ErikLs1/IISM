using System.ComponentModel.DataAnnotations;

namespace App.DTO.V1.DTO;

public class StockOrderItemDto
{
    [Required]
    public Guid ProductId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public decimal UnitCost { get; set; }
}