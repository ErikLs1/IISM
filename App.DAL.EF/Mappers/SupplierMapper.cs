using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class SupplierMapper : IMapper<SupplierDto, Supplier>
{
    private readonly StockOrderMapper _stockOrderMapper;
    private readonly ProductSupplierMapper _productSupplierMapper;

    public SupplierMapper(StockOrderMapper stockOrderMapper, ProductSupplierMapper productSupplierMapper)
    {
        _stockOrderMapper = stockOrderMapper;
        _productSupplierMapper = productSupplierMapper;
    }

    public SupplierDto? Map(Supplier? entity)
    {
        if (entity == null) return null;

        var dto = new SupplierDto()
        {
            Id = entity.Id,
            SupplierName = entity.SupplierName,
            SupplierPhoneNumber = entity.SupplierPhoneNumber,
            SupplierEmail = entity.SupplierEmail,
            SupplierAddress = entity.SupplierAddress,
            StockOrders = entity.StockOrders?
                .Select(o => _stockOrderMapper.Map(o)!)
                .ToList(),
            ProductSuppliers = entity.ProductSuppliers?
                .Select(o => _productSupplierMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public Supplier? Map(SupplierDto? dto)
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
                .Select(o => _stockOrderMapper.Map(o)!)
                .ToList();
        }

        if (dto.ProductSuppliers != null)
        {
            entity.ProductSuppliers = dto.ProductSuppliers?
                .Select(o => _productSupplierMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}