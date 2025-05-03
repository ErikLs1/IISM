using App.DAL.DTO;

namespace WebApp.Models.Index;

public class StockOrderIndexViewModel
{
    public ICollection<StockOrderDalDto> StockOrders { get; set; } = default!;

}