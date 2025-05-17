using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class StockOrderItemViewModel
{
    public ICollection<StockOrderItemBllDto> StockOrderItems { get; set; } = default!;
}