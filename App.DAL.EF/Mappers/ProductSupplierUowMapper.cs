using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class ProductSupplierUowMapper : IUowMapper<ProductSupplierDalDto, ProductSupplier>
{

    public ProductSupplierDalDto? Map(ProductSupplier? entity)
    {
        if (entity == null) return null;

        var dto = new ProductSupplierDalDto()
        {
            Id = entity.Id,
            SupplierId = entity.SupplierId,
            ProductId = entity.ProductId,
            UnitCost = entity.UnitCost,
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
            Product = entity.Product == null
                ? null
                : new ProductDalDto()
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

    public ProductSupplier? Map(ProductSupplierDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new ProductSupplier()
        {
            Id = dto.Id,
            SupplierId = dto.SupplierId,
            ProductId = dto.ProductId,
            UnitCost = dto.UnitCost,
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
            Product = dto.Product == null
                ? null
                : new Product()
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
}