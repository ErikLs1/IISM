using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.V1.DTO;

public class WarehouseDto : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string WarehouseAddress { get; set; } = default!;    
    public string WarehouseStreet { get; set; } = default!;
    public string WarehouseCity { get; set; } = default!;
    public string WarehouseState { get; set; } = default!;
    public string WarehouseCountry { get; set; } = default!;
    public string WarehousePostalCode { get; set; } = default!;
    
    [Required]
    [MaxLength(100)]
    public string WarehouseEmail { get; set; } = default!;
    
    [Required]
    public int WarehouseCapacity { get; set; } = default!;
}