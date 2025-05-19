using App.BLL.DTO;
using App.DAL.DTO;

namespace App.BLL.Mappers;

public class InventoryProductsBllMapper
{
    public InventoryProductsBllDto? Map(InventoryProductsDalDto? entity)
    {
        if (entity == null) return null;

        var res = new InventoryProductsBllDto()
        {
           
            WarehouseId = entity.WarehouseId,
            ProductId = entity.ProductId,
            ProductName = entity.ProductName,
            CategoryName = entity.CategoryName,
            ProductPrice = entity.ProductPrice,
            WarehouseCity = entity.WarehouseCity,
            WarehouseState = entity.WarehouseState,
            WarehouseCountry = entity.WarehouseCountry,
            ProductDescription = entity.ProductDescription
        };
        return res;
    }
}