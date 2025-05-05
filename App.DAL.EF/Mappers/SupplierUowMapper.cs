using App.DAL.DTO;
using App.Domain;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class SupplierUowMapper : IMapper<SupplierDalDto, Supplier>
{
    public SupplierDalDto? Map(Supplier? entity)
    {
        if (entity == null) return null;

        var dto = new SupplierDalDto()
        {
            Id = entity.Id,
            SupplierName = entity.SupplierName,
            SupplierPhoneNumber = entity.SupplierPhoneNumber,
            SupplierEmail = entity.SupplierEmail,
            SupplierAddress = entity.SupplierAddress,
            StockOrders = entity.StockOrders == null
                ? []
                : entity.StockOrders
                    .Select(o => new StockOrderDalDto()
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
                    .Select(o => new ProductSupplierDalDto()
                    {
                        Id = o.Id,
                        SupplierId = o.SupplierId,
                        ProductId = o.ProductId,
                        UnitCost = o.UnitCost
                    }).ToList()
        };

        return dto;
    }

    public Supplier? Map(SupplierDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Supplier()
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
                    .Select(o => new StockOrder()
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
                    .Select(o => new ProductSupplier()
                    {
                        Id = o.Id,
                        SupplierId = o.SupplierId,
                        ProductId = o.ProductId,
                        UnitCost = o.UnitCost
                    }).ToList();
        }

        return entity;
    }
}