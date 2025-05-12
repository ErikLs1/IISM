using App.BLL.DTO;
using App.DAL.DTO;
using Base.Contracts;

namespace App.BLL.Mappers;

public class WarehouseBllMapper : IMapper<WarehouseBllDto, WarehouseDalDto>
{
    public WarehouseBllDto? Map(WarehouseDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new WarehouseBllDto()
        {
            Id = entity.Id,
            WarehouseAddress = entity.WarehouseAddress,
            WarehouseStreet = entity.WarehouseStreet,
            WarehouseCity = entity.WarehouseCity,
            WarehouseState = entity.WarehouseState,
            WarehouseCountry = entity.WarehouseCountry,
            WarehousePostalCode = entity.WarehousePostalCode,
            WarehouseEmail = entity.WarehouseEmail,
            WarehouseCapacity = entity.WarehouseCapacity,
            StockOrders = entity.StockOrders == null
                ? []
                : entity.StockOrders
                    .Select(o => new StockOrderBllDto()
                    {
                        Id = o.Id,
                        SupplierId = o.SupplierId,
                        WarehouseId = o.WarehouseId,
                        TotalCost = o.TotalCost,
                        Status = o.Status
                    }).ToList(),
            Inventories = entity.Inventories == null
                ? []
                : entity.Inventories
                    .Select(o => new InventoryBllDto()
                    {
                        Id = o.Id,
                        ProductId = o.ProductId,
                        WarehouseId = o.WarehouseId,
                        Quantity = o.Quantity
                    }).ToList()
        };

        return dto;
    }

    public WarehouseDalDto? Map(WarehouseBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new WarehouseDalDto()
        {
            Id = dto.Id,
            WarehouseAddress = dto.WarehouseAddress,
            WarehouseStreet = dto.WarehouseStreet,
            WarehouseCity = dto.WarehouseCity,
            WarehouseState = dto.WarehouseState,
            WarehouseCountry = dto.WarehouseCountry,
            WarehousePostalCode = dto.WarehousePostalCode,
            WarehouseEmail = dto.WarehouseEmail,
            WarehouseCapacity = dto.WarehouseCapacity,
        };

        if (dto.StockOrders != null)
        {
            entity.StockOrders = dto.StockOrders == null
                ? []
                : dto.StockOrders
                    .Select(o => new StockOrderDalDto()
                    {
                        Id = o.Id
                    }).ToList();
        }

        if (dto.Inventories != null)
        {
            entity.Inventories = dto.Inventories == null
                ? []
                : dto.Inventories
                    .Select(o => new InventoryDalDto()
                    {
                        Id = o.Id,
                        ProductId = o.ProductId,
                        WarehouseId = o.WarehouseId,
                        Quantity = o.Quantity
                    }).ToList();
        }

        return entity;
    }
}