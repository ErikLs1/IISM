using App.DAL.DTO;

namespace WebApp.Models.Index;

public class OrderIndexViewModel
{
    public ICollection<OrderDalDto> Orders { get; set; } = default!;
}