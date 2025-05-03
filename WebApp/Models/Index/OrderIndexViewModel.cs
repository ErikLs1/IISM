using App.BLL.DTO;

namespace WebApp.Models.Index;

public class OrderIndexViewModel
{
    public ICollection<OrderBllDto> Orders { get; set; } = default!;
}