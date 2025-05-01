using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class InventoryUowMapper : IUowMapper<InventoryDalDto, Inventory>
{
    private readonly ProductUowMapper _productUowMapper;
    private readonly WarehouseUowMapper _warehouseUowMapper;

    public InventoryUowMapper(ProductUowMapper productUowMapper, WarehouseUowMapper warehouseUowMapper)
    {
        _productUowMapper = productUowMapper;
        _warehouseUowMapper = warehouseUowMapper;
    }

    public InventoryDalDto? Map(Inventory? entity)
    {
        if (entity == null) return null;

        var dto = new InventoryDalDto()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            WarehouseId = entity.WarehouseId,
            Quantity = entity.Quantity,
            Product = _productUowMapper.Map(entity.Product),
            Warehouse = _warehouseUowMapper.Map(entity.Warehouse),
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
            Product = _productUowMapper.Map(dto.Product),
            Warehouse = _warehouseUowMapper.Map(dto.Warehouse),
        };
        return entity;
    }
}