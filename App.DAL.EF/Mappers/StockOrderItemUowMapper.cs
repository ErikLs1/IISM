using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class StockOrderItemUowMapper : IUowMapper<StockOrderItemDalDto, StockOrderItem>
{
    private readonly StockOrderUowMapper _stockOrderUowMapper;
    private readonly ProductUowMapper _productUowMapper;

    public StockOrderItemUowMapper(StockOrderUowMapper stockOrderUowMapper, ProductUowMapper productUowMapper)
    {
        _stockOrderUowMapper = stockOrderUowMapper;
        _productUowMapper = productUowMapper;
    }

    public StockOrderItemDalDto? Map(StockOrderItem? entity)
    {
        if (entity == null) return null;

        var dto = new StockOrderItemDalDto()
        {
            Id = entity.Id,
            StockOrderId = entity.StockOrderId,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity,
            Cost = entity.Cost,
            StockOrder = _stockOrderUowMapper.Map(entity.StockOrder),
            Product = _productUowMapper.Map(entity.Product),
        };

        return dto;
    }

    public StockOrderItem? Map(StockOrderItemDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new StockOrderItem()
        {
            Id = dto.Id,
            StockOrderId = dto.StockOrderId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Cost = dto.Cost,
            StockOrder = _stockOrderUowMapper.Map(dto.StockOrder),
            Product = _productUowMapper.Map(dto.Product),
        };

        return entity;
    }
}