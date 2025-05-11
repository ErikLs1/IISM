using System.ComponentModel.DataAnnotations;

namespace App.DTO.V1.DTO;

public class CategoryCreateDto
{
    [Required]
    [MaxLength(50)]
    public string CategoryName { get; set; } = default!;
    
    [Required]
    [MaxLength(250)]
    public string CategoryDescription { get; set; } = default!;
}