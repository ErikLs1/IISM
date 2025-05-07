using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.V1.DTO;

public class WarehouseDto : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string WarehouseAddress { get; set; } = default!;
    
    [Required]
    [MaxLength(100)]
    public string WarehouseEmail { get; set; } = default!;
    
    [Required]
    public int WarehouseCapacity { get; set; } = default!;
}