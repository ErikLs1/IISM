using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

/// <summary>
/// 
/// </summary>
public class InventoryViewModelMapper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public InventoryMvcDto Map(InventoryBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new InventoryMvcDto()
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            ProductName = dto.Product?.ProductName,
            ProductDescription = dto.Product?.ProductDescription,
            WarehouseAddress = dto.Warehouse?.WarehouseAddress
        };
    }
    
}