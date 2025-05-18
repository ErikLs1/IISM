using App.BLL.DTO;
using App.DTO.V1.DTO;

namespace App.DTO.V1.Mappers;

public class WarehouseInventoryItemsMapper
{
    public WarehouseInventoryItemsDto? Map(InventoryBllDto? entity)
    {
        if (entity == null) return null;

        var res = new WarehouseInventoryItemsDto()
        {
           
            ProductId = entity.ProductId,
            ProductName = entity.Product!.ProductName,
            ProductDescription = entity.Product!.ProductDescription,
            Quantity = entity.Quantity
        };
        return res;
    }
}