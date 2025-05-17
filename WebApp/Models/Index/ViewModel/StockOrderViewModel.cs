using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class StockOrderViewModel
{
    public ICollection<StockOrderBllDto> StockOrders { get; set; } = default!;

}