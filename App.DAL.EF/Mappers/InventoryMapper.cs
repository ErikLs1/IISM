using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class InventoryMapper : IMapper<InventoryDto, Inventory>
{
    private readonly ProductMapper _productMapper;
    private readonly WarehouseMapper _warehouseMapper;

    public InventoryMapper(ProductMapper productMapper, WarehouseMapper warehouseMapper)
    {
        _productMapper = productMapper;
        _warehouseMapper = warehouseMapper;
    }

    public InventoryDto? Map(Inventory? entity)
    {
        if (entity == null) return null;

        var dto = new InventoryDto()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            WarehouseId = entity.WarehouseId,
            Quantity = entity.Quantity,
            Product = _productMapper.Map(entity.Product),
            Warehouse = _warehouseMapper.Map(entity.Warehouse),
        };

        return dto;
    }

    public Inventory? Map(InventoryDto? dto)
    {
        if (dto == null) return null;

        var entity = new Inventory()
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            WarehouseId = dto.WarehouseId,
            Quantity = dto.Quantity,
            Product = _productMapper.Map(dto.Product),
            Warehouse = _warehouseMapper.Map(dto.Warehouse),
        };
        return entity;
    }
}