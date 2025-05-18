using App.BLL.DTO;
using App.DTO.V1.DTO;

namespace App.DTO.V1.Mappers;

public class InventoryProductsMapper
{
    public InventoryProductsDto? Map(InventoryProductsBllDto? entity)
    {
        if (entity == null) return null;

        var res = new InventoryProductsDto()
        {
            WarehouseId = entity.WarehouseId,
            ProductId = entity.ProductId,
            ProductName = entity.ProductName,
            CategoryName = entity.CategoryName,
            ProductPrice = Math.Round(entity.ProductPrice * 1.5m, 2),
            WarehouseCity = entity.WarehouseCity,
            WarehouseState = entity.WarehouseState,
            WarehouseCountry = entity.WarehouseCountry,
            ProductDescription = entity.ProductDescription
        };
        return res;
    }
}