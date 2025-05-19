using App.DAL.DTO;
using App.Domain;

namespace App.DAL.EF.Mappers;

public class InventoryProductsUowMapper
{
    public InventoryProductsDalDto? Map(Inventory? entity)
    {
        if (entity == null) return null;

        var dto = new InventoryProductsDalDto()
        {
            WarehouseId = entity.WarehouseId,
            ProductId = entity.ProductId,
            ProductName = entity.Product!.ProductName,
            CategoryName = entity.Product.Category!.CategoryName,
            ProductPrice = entity.Product.ProductPrice,
            WarehouseCity = entity.Warehouse!.WarehouseCity,
            WarehouseState = entity.Warehouse.WarehouseState,
            WarehouseCountry = entity.Warehouse.WarehouseCountry,
            ProductDescription = entity.Product.ProductDescription
        };

        return dto;
    }
}