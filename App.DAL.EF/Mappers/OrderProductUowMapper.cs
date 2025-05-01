using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class OrderProductUowMapper : IUowMapper<OrderProductDalDto, OrderProduct>
{
    private readonly ProductUowMapper _productUowMapper;
    private readonly OrderUowMapper _orderUowMapper;
    private readonly RefundUowMapper _refundUowMapper;

    public OrderProductUowMapper(ProductUowMapper productUowMapper, OrderUowMapper orderUowMapper, RefundUowMapper refundUowMapper)
    {
        _productUowMapper = productUowMapper;
        _orderUowMapper = orderUowMapper;
        _refundUowMapper = refundUowMapper;
    }

    public OrderProductDalDto? Map(OrderProduct? entity)
    {
        if (entity == null) return null;

        var dto = new OrderProductDalDto()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            OrderId = entity.OrderId,
            Quantity = entity.Quantity,
            TotalPrice = entity.TotalPrice,
            Product = _productUowMapper.Map(entity.Product),
            Order = _orderUowMapper.Map(entity.Order),
            Refunds = entity.Refunds?
                .Select(o => _refundUowMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public OrderProduct? Map(OrderProductDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new OrderProduct()
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            OrderId = dto.OrderId,
            Quantity = dto.Quantity,
            TotalPrice = dto.TotalPrice,
            Product = _productUowMapper.Map(dto.Product),
            Order = _orderUowMapper.Map(dto.Order)
        };

        if (dto.Refunds != null)
        {
            entity.Refunds = dto.Refunds?
                .Select(o => _refundUowMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}