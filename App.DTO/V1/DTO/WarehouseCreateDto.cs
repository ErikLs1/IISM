using System.ComponentModel.DataAnnotations;

namespace App.DTO.V1.DTO;

public class WarehouseCreateDto
{
    [Required]
    [MaxLength(100)]
    public string WarehouseAddress { get; set; } = default!;
    
    [Required]
    [MaxLength(100)]
    public string WarehouseEmail { get; set; } = default!;
    
    [Required]
    public int WarehouseCapacity { get; set; } = default!;
}