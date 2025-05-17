using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class StockOrderItemViewModel
{
    public ICollection<StockOrderItemMvcDto> StockOrderItems { get; set; } = default!;
}