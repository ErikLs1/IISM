using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class StockOrderBllMapper : IBllMapper<StockOrderBllDto, StockOrderDalDto>
{
    public StockOrderDalDto? Map(StockOrderBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new StockOrderDalDto()
        {
            Id = dto.Id,
            SupplierId = dto.SupplierId,
            WarehouseId = dto.WarehouseId,
            TotalCost = dto.TotalCost,
            Status = dto.Status,
            Supplier = dto.Supplier == null
                ? null
                : new SupplierDalDto()
                {
                    Id = dto.Supplier.Id,
                    SupplierName = dto.Supplier.SupplierName,
                    SupplierPhoneNumber = dto.Supplier.SupplierPhoneNumber,
                    SupplierEmail = dto.Supplier.SupplierEmail,
                    SupplierAddress = dto.Supplier.SupplierAddress
                },
            Warehouse = dto.Warehouse == null
                ? null
                : new WarehouseDalDto()
                {
                    Id = dto.Warehouse.Id,
                    WarehouseAddress = dto.Warehouse.WarehouseAddress,
                    WarehouseEmail = dto.Warehouse.WarehouseEmail,
                    WarehouseCapacity = dto.Warehouse.WarehouseCapacity
                },
        };

        if (dto.StockOrderItems != null)
        {
            entity.StockOrderItems = dto.StockOrderItems == null
                ? []
                : dto.StockOrderItems
                    .Select(o => new StockOrderItemDalDto()
                    {
                        Id = o.Id,
                        StockOrderId = o.StockOrderId,
                        ProductId = o.ProductId,
                        Quantity = o.Quantity,
                        Cost = o.Cost
                    }).ToList();
        }

        return entity;
    }

    public StockOrderBllDto? Map(StockOrderDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new StockOrderBllDto()
        {
            Id = entity.Id,
            SupplierId = entity.SupplierId,
            WarehouseId = entity.WarehouseId,
            TotalCost = entity.TotalCost,
            Status = entity.Status,
            Supplier = entity.Supplier == null
                ? null
                : new SupplierBllDto()
                {
                    Id = entity.Supplier.Id,
                    SupplierName = entity.Supplier.SupplierName,
                    SupplierPhoneNumber = entity.Supplier.SupplierPhoneNumber,
                    SupplierEmail = entity.Supplier.SupplierEmail,
                    SupplierAddress = entity.Supplier.SupplierAddress
                },
            Warehouse = entity.Warehouse == null
                ? null
                : new WarehouseBllDto()
                {
                    Id = entity.Warehouse.Id,
                    WarehouseAddress = entity.Warehouse.WarehouseAddress,
                    WarehouseEmail = entity.Warehouse.WarehouseEmail,
                    WarehouseCapacity = entity.Warehouse.WarehouseCapacity
                },
            StockOrderItems = entity.StockOrderItems == null
                ? []
                : entity.StockOrderItems
                    .Select(o => new StockOrderItemBllDto()
                    {
                        Id = o.Id,
                        StockOrderId = o.StockOrderId,
                        ProductId = o.ProductId,
                        Quantity = o.Quantity,
                        Cost = o.Cost
                    }).ToList()
        };

        return dto;
    }
}