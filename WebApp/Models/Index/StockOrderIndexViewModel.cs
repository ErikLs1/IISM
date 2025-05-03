using App.BLL.DTO;

namespace WebApp.Models.Index;

public class StockOrderIndexViewModel
{
    public ICollection<StockOrderBllDto> StockOrders { get; set; } = default!;

}