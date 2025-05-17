using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

/// <summary>
/// 
/// </summary>
public class StockOrderItemViewModelMapper
{
    public StockOrderItemMvcDto Map(StockOrderItemBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new StockOrderItemMvcDto()
        {
        };
    }
}