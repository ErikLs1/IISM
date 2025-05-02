using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class InventoryUowMapper : IUowMapper<InventoryDalDto, Inventory>
{
    public InventoryDalDto? Map(Inventory? entity)
    {
        if (entity == null) return null;

        var dto = new InventoryDalDto()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            WarehouseId = entity.WarehouseId,
            Quantity = entity.Quantity,
            Product = entity.Product == null
                ? null
                : new ProductDalDto()
                {
                    Id = entity.Product.Id,
                    CategoryId = entity.Product.CategoryId,
                    ProductName = entity.Product.ProductName,
                    ProductDescription = entity.Product.ProductDescription,
                    ProductPrice = entity.Product.ProductPrice,
                    ProductStatus = entity.Product.ProductStatus
                },
            Warehouse = entity.Warehouse == null
                ? null
                : new WarehouseDalDto()
                {
                    Id = entity.Warehouse.Id,
                    WarehouseAddress = entity.Warehouse.WarehouseAddress,
                    WarehouseEmail = entity.Warehouse.WarehouseEmail,
                    WarehouseCapacity = entity.Warehouse.WarehouseCapacity
                }
        };

        return dto;
    }

    public Inventory? Map(InventoryDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Inventory()
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            WarehouseId = dto.WarehouseId,
            Quantity = dto.Quantity,
            Product = dto.Product == null
                ? null
                : new Product()
                {
                    Id = dto.Product.Id,
                    CategoryId = dto.Product.CategoryId,
                    ProductName = dto.Product.ProductName,
                    ProductDescription = dto.Product.ProductDescription,
                    ProductPrice = dto.Product.ProductPrice,
                    ProductStatus = dto.Product.ProductStatus
                },
            Warehouse = dto.Warehouse == null
                ? null
                : new Warehouse()
                {
                    Id = dto.Warehouse.Id,
                    WarehouseAddress = dto.Warehouse.WarehouseAddress,
                    WarehouseEmail = dto.Warehouse.WarehouseEmail,
                    WarehouseCapacity = dto.Warehouse.WarehouseCapacity,
                },
        };
        return entity;
    }
}