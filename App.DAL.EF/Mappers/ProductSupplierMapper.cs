using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class ProductSupplierMapper : IMapper<ProductSupplierDto, ProductSupplier>
{
    private readonly SupplierMapper _supplierMapper;
    private readonly ProductMapper _productMapper;

    public ProductSupplierMapper(SupplierMapper supplierMapper, ProductMapper productMapper)
    {
        _supplierMapper = supplierMapper;
        _productMapper = productMapper;
    }

    public ProductSupplierDto? Map(ProductSupplier? entity)
    {
        if (entity == null) return null;

        var dto = new ProductSupplierDto()
        {
            Id = entity.Id,
            SupplierId = entity.SupplierId,
            ProductId = entity.ProductId,
            UnitCost = entity.UnitCost,
            Supplier = _supplierMapper.Map(entity.Supplier),
            Product = _productMapper.Map(entity.Product)
        };

        return dto;
    }

    public ProductSupplier? Map(ProductSupplierDto? dto)
    {
        if (dto == null) return null;

        var entity = new ProductSupplier()
        {
            Id = dto.Id,
            SupplierId = dto.SupplierId,
            ProductId = dto.ProductId,
            UnitCost = dto.UnitCost,
            Supplier = _supplierMapper.Map(dto.Supplier),
            Product = _productMapper.Map(dto.Product)
        };

        return entity;
    }
}