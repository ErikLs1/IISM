using System.ComponentModel.DataAnnotations;

namespace App.DTO.V1.DTO;

public class ChangeOrderStatusDto
{
    [Required] 
    public Guid OrderId { get; set; }
    [Required] 
    public string OrderStatus { get; set; } = default!;
}