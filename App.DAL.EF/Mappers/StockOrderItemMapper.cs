using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class StockOrderItemMapper : IMapper<StockOrderItemDto, StockOrderItem>
{
    private readonly StockOrderMapper _stockOrderMapper;
    private readonly ProductMapper _productMapper;

    public StockOrderItemMapper(StockOrderMapper stockOrderMapper, ProductMapper productMapper)
    {
        _stockOrderMapper = stockOrderMapper;
        _productMapper = productMapper;
    }

    public StockOrderItemDto? Map(StockOrderItem? entity)
    {
        if (entity == null) return null;

        var dto = new StockOrderItemDto()
        {
            Id = entity.Id,
            StockOrderId = entity.StockOrderId,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity,
            Cost = entity.Cost,
            StockOrder = _stockOrderMapper.Map(entity.StockOrder),
            Product = _productMapper.Map(entity.Product),
        };

        return dto;
    }

    public StockOrderItem? Map(StockOrderItemDto? dto)
    {
        if (dto == null) return null;

        var entity = new StockOrderItem()
        {
            Id = dto.Id,
            StockOrderId = dto.StockOrderId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Cost = dto.Cost,
            StockOrder = _stockOrderMapper.Map(dto.StockOrder),
            Product = _productMapper.Map(dto.Product),
        };

        return entity;
    }
}