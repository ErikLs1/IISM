using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class StockOrderMapper : IMapper<StockOrderDto, StockOrder>
{
    private readonly SupplierMapper _supplierMapper;
    private readonly WarehouseMapper _warehouseMapper;
    private readonly StockOrderItemMapper _stockOrderItemMapper;

    public StockOrderMapper(SupplierMapper supplierMapper, WarehouseMapper warehouseMapper, StockOrderItemMapper stockOrderItemMapper)
    {
        _supplierMapper = supplierMapper;
        _warehouseMapper = warehouseMapper;
        _stockOrderItemMapper = stockOrderItemMapper;
    }

    public StockOrderDto? Map(StockOrder? entity)
    {
        if (entity == null) return null;

        var dto = new StockOrderDto()
        {
            Id = entity.Id,
            SupplierId = entity.SupplierId,
            WarehouseId = entity.WarehouseId,
            TotalCost = entity.TotalCost,
            Status = entity.Status,
            Supplier = _supplierMapper.Map(entity.Supplier),
            Warehouse = _warehouseMapper.Map(entity.Warehouse),
            StockOrderItems = entity.StockOrderItems?
                .Select(o => _stockOrderItemMapper.Map(o)!)
                .ToList()
        };

        return dto;
    }

    public StockOrder? Map(StockOrderDto? dto)
    {
        if (dto == null) return null;

        var entity = new StockOrder()
        {
            Id = dto.Id,
            SupplierId = dto.SupplierId,
            WarehouseId = dto.WarehouseId,
            TotalCost = dto.TotalCost,
            Status = dto.Status,
            Supplier = _supplierMapper.Map(dto.Supplier),
            Warehouse = _warehouseMapper.Map(dto.Warehouse),
        };

        if (dto.StockOrderItems != null)
        {
            entity.StockOrderItems = dto.StockOrderItems?
                .Select(o => _stockOrderItemMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}