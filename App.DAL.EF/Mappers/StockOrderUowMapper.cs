using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class StockOrderUowMapper : IUowMapper<StockOrderDalDto, StockOrder>
{
    private readonly SupplierUowMapper _supplierUowMapper;
    private readonly WarehouseUowMapper _warehouseUowMapper;
    private readonly StockOrderItemUowMapper _stockOrderItemUowMapper;

    public StockOrderUowMapper(SupplierUowMapper supplierUowMapper, WarehouseUowMapper warehouseUowMapper, StockOrderItemUowMapper stockOrderItemUowMapper)
    {
        _supplierUowMapper = supplierUowMapper;
        _warehouseUowMapper = warehouseUowMapper;
        _stockOrderItemUowMapper = stockOrderItemUowMapper;
    }

    public StockOrderDalDto? Map(StockOrder? entity)
    {
        if (entity == null) return null;

        var dto = new StockOrderDalDto()
        {
            Id = entity.Id,
            SupplierId = entity.SupplierId,
            WarehouseId = entity.WarehouseId,
            TotalCost = entity.TotalCost,
            Status = entity.Status,
            Supplier = _supplierUowMapper.Map(entity.Supplier),
            Warehouse = _warehouseUowMapper.Map(entity.Warehouse),
            StockOrderItems = entity.StockOrderItems?
                .Select(o => _stockOrderItemUowMapper.Map(o)!)
                .ToList()
        };

        return dto;
    }

    public StockOrder? Map(StockOrderDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new StockOrder()
        {
            Id = dto.Id,
            SupplierId = dto.SupplierId,
            WarehouseId = dto.WarehouseId,
            TotalCost = dto.TotalCost,
            Status = dto.Status,
            Supplier = _supplierUowMapper.Map(dto.Supplier),
            Warehouse = _warehouseUowMapper.Map(dto.Warehouse),
        };

        if (dto.StockOrderItems != null)
        {
            entity.StockOrderItems = dto.StockOrderItems?
                .Select(o => _stockOrderItemUowMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}