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
        
        return new StockOrderItemMvcDto
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            Cost = dto.Cost,
            ProductName = dto.Product!.ProductName
        };
    }
    
    /*public StockOrderItemBllDto Map(StockOrderItemMvcDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new StockOrderItemBllDto
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            Cost = dto.Cost,
            ProductName = dto.Product!.ProductName
        };
    }*/
}