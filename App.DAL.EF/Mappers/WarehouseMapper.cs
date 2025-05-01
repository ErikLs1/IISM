using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class WarehouseMapper : IMapper<WarehouseDto, Warehouse>
{
    private readonly StockOrderMapper _stockOrderMapper;
    private readonly InventoryMapper _inventoryMapper;

    public WarehouseMapper(StockOrderMapper stockOrderMapper, InventoryMapper inventoryMapper)
    {
        _stockOrderMapper = stockOrderMapper;
        _inventoryMapper = inventoryMapper;
    }

    public WarehouseDto? Map(Warehouse? entity)
    {
        if (entity == null) return null;

        var dto = new WarehouseDto()
        {
            Id = entity.Id,
            WarehouseAddress = entity.WarehouseAddress,
            WarehouseEmail = entity.WarehouseEmail,
            WarehouseCapacity = entity.WarehouseCapacity,
            StockOrders = entity.StockOrders?
                .Select(o => _stockOrderMapper.Map(o)!)
                .ToList(),
            Inventories = entity.Inventories?
                .Select(o => _inventoryMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public Warehouse? Map(WarehouseDto? dto)
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
                .Select(o => _stockOrderMapper.Map(o)!)
                .ToList();
        }

        if (dto.Inventories != null)
        {
            entity.Inventories = dto.Inventories?
                .Select(o => _inventoryMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}