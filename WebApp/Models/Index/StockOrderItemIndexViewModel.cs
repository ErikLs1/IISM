using App.DAL.DTO;

namespace WebApp.Models.Index;

public class StockOrderItemIndexViewModel
{
    public ICollection<StockOrderItemDalDto> StockOrderItems { get; set; } = default!;
}