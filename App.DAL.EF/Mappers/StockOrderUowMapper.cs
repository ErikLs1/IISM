using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class StockOrderUowMapper : IUowMapper<StockOrderDalDto, StockOrder>
{
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
            Supplier = entity.Supplier == null
                ? null
                : new SupplierDalDto()
                {
                    Id = entity.Supplier.Id,
                    SupplierName = entity.Supplier.SupplierName,
                    SupplierPhoneNumber = entity.Supplier.SupplierPhoneNumber,
                    SupplierEmail = entity.Supplier.SupplierEmail,
                    SupplierAddress = entity.Supplier.SupplierAddress
                },
            Warehouse = entity.Warehouse == null
                ? null
                : new WarehouseDalDto()
                {
                    Id = entity.Warehouse.Id,
                    WarehouseAddress = entity.Warehouse.WarehouseAddress,
                    WarehouseEmail = entity.Warehouse.WarehouseEmail,
                    WarehouseCapacity = entity.Warehouse.WarehouseCapacity
                },
            StockOrderItems = entity.StockOrderItems == null
                ? []
                : entity.StockOrderItems
                    .Select(o => new StockOrderItemDalDto()
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
            Supplier = dto.Supplier == null
                ? null
                : new Supplier()
                {
                    Id = dto.Supplier.Id,
                    SupplierName = dto.Supplier.SupplierName,
                    SupplierPhoneNumber = dto.Supplier.SupplierPhoneNumber,
                    SupplierEmail = dto.Supplier.SupplierEmail,
                    SupplierAddress = dto.Supplier.SupplierAddress
                },
            Warehouse = dto.Warehouse == null
                ? null
                : new Warehouse()
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
                    .Select(o => new StockOrderItem()
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
}