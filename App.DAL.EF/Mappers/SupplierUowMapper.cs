using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class SupplierUowMapper : IUowMapper<SupplierDalDto, Supplier>
{
    private readonly StockOrderUowMapper _stockOrderUowMapper;
    private readonly ProductSupplierUowMapper _productSupplierUowMapper;

    public SupplierUowMapper(StockOrderUowMapper stockOrderUowMapper, ProductSupplierUowMapper productSupplierUowMapper)
    {
        _stockOrderUowMapper = stockOrderUowMapper;
        _productSupplierUowMapper = productSupplierUowMapper;
    }

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
            StockOrders = entity.StockOrders?
                .Select(o => _stockOrderUowMapper.Map(o)!)
                .ToList(),
            ProductSuppliers = entity.ProductSuppliers?
                .Select(o => _productSupplierUowMapper.Map(o)!)
                .ToList(),
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
            entity.StockOrders = dto.StockOrders?
                .Select(o => _stockOrderUowMapper.Map(o)!)
                .ToList();
        }

        if (dto.ProductSuppliers != null)
        {
            entity.ProductSuppliers = dto.ProductSuppliers?
                .Select(o => _productSupplierUowMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}