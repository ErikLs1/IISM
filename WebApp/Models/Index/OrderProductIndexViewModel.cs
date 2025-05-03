using App.DAL.DTO;

namespace WebApp.Models.Index;

public class OrderProductIndexViewModel
{
    public ICollection<OrderProductDalDto> OrderProducts { get; set; } = default!;
 
}