using System.ComponentModel.DataAnnotations;

namespace App.DTO.V1.DTO;

public class WarehouseCreateDto
{
    [Required]
    [MaxLength(100)]
    public string WarehouseAddress { get; set; } = default!;
    [Required]
    [MaxLength(100)]
    public string WarehouseStreet { get; set; } = default!;
    
    [Required]
    [MaxLength(100)]
    public string WarehouseCity { get; set; } = default!;
    
    [Required]
    [MaxLength(50)]
    public string WarehouseState { get; set; } = default!;
    
    [Required]
    [MaxLength(100)]
    public string WarehouseCountry { get; set; } = default!;
    
    [Required]
    [MaxLength(30)]
    public string WarehousePostalCode { get; set; } = default!;
    
    [Required]
    [MaxLength(100)]
    public string WarehouseEmail { get; set; } = default!;
    
    [Required]
    public int WarehouseCapacity { get; set; }
}