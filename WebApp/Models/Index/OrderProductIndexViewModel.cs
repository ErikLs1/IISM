using App.BLL.DTO;

namespace WebApp.Models.Index;

public class OrderProductIndexViewModel
{
    public ICollection<OrderProductBllDto> OrderProducts { get; set; } = default!;
 
}