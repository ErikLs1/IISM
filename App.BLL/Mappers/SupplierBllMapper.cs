using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class SupplierBllMapper : IBllMapper<SupplierBllDto, SupplierDalDto>
{
    public SupplierDalDto? Map(SupplierBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new SupplierDalDto()
        {
            Id = dto.Id,
            SupplierName = dto.SupplierName,
            SupplierPhoneNumber = dto.SupplierPhoneNumber,
            SupplierEmail = dto.SupplierEmail,
            SupplierAddress = dto.SupplierAddress
        };

        if (dto.StockOrders != null)
        {
            entity.StockOrders = dto.StockOrders == null
                ? []
                : dto.StockOrders
                    .Select(o => new StockOrderDalDto()
                    {
                        Id = o.Id,
                        SupplierId = o.SupplierId,
                        WarehouseId = o.WarehouseId,
                        TotalCost = o.TotalCost,
                        Status = o.Status
                    }).ToList();
        }

        if (dto.ProductSuppliers != null)
        {
            entity.ProductSuppliers = dto.ProductSuppliers == null
                ? []
                : dto.ProductSuppliers
                    .Select(o => new ProductSupplierDalDto()
                    {
                        Id = o.Id,
                        SupplierId = o.SupplierId,
                        ProductId = o.ProductId,
                        UnitCost = o.UnitCost
                    }).ToList();
        }

        return entity;
    }

    public SupplierBllDto? Map(SupplierDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new SupplierBllDto()
        {
            Id = entity.Id,
            SupplierName = entity.SupplierName,
            SupplierPhoneNumber = entity.SupplierPhoneNumber,
            SupplierEmail = entity.SupplierEmail,
            SupplierAddress = entity.SupplierAddress,
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
            ProductSuppliers = entity.ProductSuppliers == null
                ? []
                : entity.ProductSuppliers
                    .Select(o => new ProductSupplierBllDto()
                    {
                        Id = o.Id,
                        SupplierId = o.SupplierId,
                        ProductId = o.ProductId,
                        UnitCost = o.UnitCost
                    }).ToList()
        };

        return dto;
    }
}