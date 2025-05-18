using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

public class InventoryViewModelMapper
{
    public InventoryMvcDto Map(InventoryBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new InventoryMvcDto()
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            WarehouseId = dto.WarehouseId,
            Quantity = dto.Quantity,
            ProductName = dto.Product?.ProductName,
            ProductDescription = dto.Product?.ProductDescription,
            WarehouseAddress = dto.Warehouse?.WarehouseAddress
        };
    }
    
    public InventoryBllDto Map(InventoryMvcDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new InventoryBllDto()
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            ProductId = dto.ProductId,
            WarehouseId = dto.WarehouseId
        };
    }
}