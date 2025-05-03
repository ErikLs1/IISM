using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class ProductSupplierBllMapper : IBllMapper<ProductSupplierBllDto, ProductSupplierDalDto>
{
    public ProductSupplierDalDto? Map(ProductSupplierBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new ProductSupplierDalDto()
        {
            Id = dto.Id,
            SupplierId = dto.SupplierId,
            ProductId = dto.ProductId,
            UnitCost = dto.UnitCost,
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
            Product = dto.Product == null
                ? null
                : new ProductDalDto()
                {
                    Id = dto.Product.Id,
                    CategoryId = dto.Product.CategoryId,
                    ProductName = dto.Product.ProductName,
                    ProductDescription = dto.Product.ProductDescription,
                    ProductPrice = dto.Product.ProductPrice,
                    ProductStatus = dto.Product.ProductStatus,
                },
        };

        return entity;
    }

    public ProductSupplierBllDto? Map(ProductSupplierDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new ProductSupplierBllDto()
        {
            Id = entity.Id,
            SupplierId = entity.SupplierId,
            ProductId = entity.ProductId,
            UnitCost = entity.UnitCost,
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
            Product = entity.Product == null
                ? null
                : new ProductBllDto()
                {
                    Id = entity.Product.Id,
                    CategoryId = entity.Product.CategoryId,
                    ProductName = entity.Product.ProductName,
                    ProductDescription = entity.Product.ProductDescription,
                    ProductPrice = entity.Product.ProductPrice,
                    ProductStatus = entity.Product.ProductStatus,
                },
        };

        return dto;
    }
}