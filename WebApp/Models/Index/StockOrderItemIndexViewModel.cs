using App.BLL.DTO;

namespace WebApp.Models.Index;

public class StockOrderItemIndexViewModel
{
    public ICollection<StockOrderItemBllDto> StockOrderItems { get; set; } = default!;
}