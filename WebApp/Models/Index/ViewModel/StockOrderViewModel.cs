using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class StockOrderViewModel
{
    public ICollection<StockOrderMvcDto> StockOrders { get; set; } = default!;

}