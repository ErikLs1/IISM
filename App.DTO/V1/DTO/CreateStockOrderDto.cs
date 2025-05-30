using System.ComponentModel.DataAnnotations;

namespace App.DTO.V1.DTO;

public class CreateStockOrderDto
{
    [Required]
    public Guid SupplierId { get; set; }
    
    [Required]
    public Guid WarehouseId { get; set; }
    
    [Required, MinLength(1)]
    public List<StockOrderItemDto> Products { get; set; } = default!;
}