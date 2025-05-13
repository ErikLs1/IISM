using App.BLL.DTO;
using App.DTO.V1.DTO;
using Base.Contracts;

namespace App.DTO.V1.Mappers;

public class ProductSuppliersMapper : IMapper<ProductSupplierDto, ProductSupplierBllDto>
{
    public ProductSupplierDto? Map(ProductSupplierBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new ProductSupplierDto()
        {
            Id = dto.Id,
            SupplierId = dto.SupplierId,
            ProductId = dto.ProductId,
            UnitCost = dto.UnitCost,
            Supplier = dto.Supplier == null
                ? null
                : new SupplierDto()
                {
                    SupplierName = dto.Supplier.SupplierName,
                    SupplierPhoneNumber = dto.Supplier.SupplierPhoneNumber,
                    SupplierEmail = dto.Supplier.SupplierEmail,
                    SupplierAddress = dto.Supplier.SupplierAddress
                },
            Product = dto.Product == null
                ? null
                : new ProductDto()
                {
                    ProductName = dto.Product.ProductName,
                    ProductDescription = dto.Product.ProductDescription
                },
        };

        return entity;
    }

    public ProductSupplierBllDto? Map(ProductSupplierDto? entity)
    {
        /*if (entity == null) return null;

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

        return dto;*/

        throw new NotImplementedException();
    }
}