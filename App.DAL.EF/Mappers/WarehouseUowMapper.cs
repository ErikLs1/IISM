using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class WarehouseUowMapper : IUowMapper<WarehouseDalDto, Warehouse>
{
    private readonly StockOrderUowMapper _stockOrderUowMapper;
    private readonly InventoryUowMapper _inventoryUowMapper;

    public WarehouseUowMapper(StockOrderUowMapper stockOrderUowMapper, InventoryUowMapper inventoryUowMapper)
    {
        _stockOrderUowMapper = stockOrderUowMapper;
        _inventoryUowMapper = inventoryUowMapper;
    }

    public WarehouseDalDto? Map(Warehouse? entity)
    {
        if (entity == null) return null;

        var dto = new WarehouseDalDto()
        {
            Id = entity.Id,
            WarehouseAddress = entity.WarehouseAddress,
            WarehouseEmail = entity.WarehouseEmail,
            WarehouseCapacity = entity.WarehouseCapacity,
            StockOrders = entity.StockOrders?
                .Select(o => _stockOrderUowMapper.Map(o)!)
                .ToList(),
            Inventories = entity.Inventories?
                .Select(o => _inventoryUowMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public Warehouse? Map(WarehouseDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Warehouse()
        {
            Id = dto.Id,
            WarehouseAddress = dto.WarehouseAddress,
            WarehouseEmail = dto.WarehouseEmail,
            WarehouseCapacity = dto.WarehouseCapacity,
        };

        if (dto.StockOrders != null)
        {
            entity.StockOrders = dto.StockOrders?
                .Select(o => _stockOrderUowMapper.Map(o)!)
                .ToList();
        }

        if (dto.Inventories != null)
        {
            entity.Inventories = dto.Inventories?
                .Select(o => _inventoryUowMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}