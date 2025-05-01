using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class ProductSupplierUowMapper : IUowMapper<ProductSupplierDalDto, ProductSupplier>
{
    private readonly SupplierUowMapper _supplierUowMapper;
    private readonly ProductUowMapper _productUowMapper;

    public ProductSupplierUowMapper(SupplierUowMapper supplierUowMapper, ProductUowMapper productUowMapper)
    {
        _supplierUowMapper = supplierUowMapper;
        _productUowMapper = productUowMapper;
    }

    public ProductSupplierDalDto? Map(ProductSupplier? entity)
    {
        if (entity == null) return null;

        var dto = new ProductSupplierDalDto()
        {
            Id = entity.Id,
            SupplierId = entity.SupplierId,
            ProductId = entity.ProductId,
            UnitCost = entity.UnitCost,
            Supplier = _supplierUowMapper.Map(entity.Supplier),
            Product = _productUowMapper.Map(entity.Product)
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
            Supplier = _supplierUowMapper.Map(dto.Supplier),
            Product = _productUowMapper.Map(dto.Product)
        };

        return entity;
    }
}