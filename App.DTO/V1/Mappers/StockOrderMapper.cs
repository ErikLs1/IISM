using App.BLL.DTO;
using App.DTO.V1.DTO;
using Base.Contracts;

namespace App.DTO.V1.Mappers;

public class StockOrderMapper : IMapper<StockOrderDto, StockOrderBllDto>
{
    public StockOrderDto? Map(StockOrderBllDto? entity)
    {
        if (entity == null) return null;

        var dto = new StockOrderDto()
        {
            Id = entity.Id,
            SupplierId = entity.SupplierId,
            WarehouseId = entity.WarehouseId,
            TotalCost = entity.TotalCost,
            Status = entity.Status,
            Supplier = entity.Supplier == null
                ? null
                : new SupplierDto()
                {
                    SupplierName = entity.Supplier.SupplierName,
                    SupplierPhoneNumber = entity.Supplier.SupplierPhoneNumber,
                    SupplierEmail = entity.Supplier.SupplierEmail,
                    SupplierAddress = entity.Supplier.SupplierAddress
                },
            Warehouse = entity.Warehouse == null
                ? null
                : new WarehouseDto()
                {
                    Id = entity.Warehouse.Id,
                    WarehouseAddress = entity.Warehouse.WarehouseAddress,
                    WarehouseEmail = entity.Warehouse.WarehouseEmail,
                    WarehouseCapacity = entity.Warehouse.WarehouseCapacity
                },
            StockOrderItems = entity.StockOrderItems == null
                ? []
                : entity.StockOrderItems
                    .Select(o => new StockOrderItemDto()
                    {
                        ProductId = o.ProductId,
                        Quantity = o.Quantity,
                    }).ToList()
        };

        return dto;
    }

    public StockOrderBllDto? Map(StockOrderDto? entity)
    {
        throw new NotImplementedException();
    }
    
}