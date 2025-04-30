using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class OrderProductMapper : IMapper<OrderProductDto, OrderProduct>
{
    private readonly ProductMapper _productMapper;
    private readonly OrderMapper _orderMapper;
    private readonly RefundMapper _refundMapper;

    public OrderProductMapper(ProductMapper productMapper, OrderMapper orderMapper, RefundMapper refundMapper)
    {
        _productMapper = productMapper;
        _orderMapper = orderMapper;
        _refundMapper = refundMapper;
    }

    public OrderProductDto? Map(OrderProduct? entity)
    {
        if (entity == null) return null;

        var dto = new OrderProductDto()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            OrderId = entity.OrderId,
            Quantity = entity.Quantity,
            TotalPrice = entity.TotalPrice,
            Product = _productMapper.Map(entity.Product),
            Order = _orderMapper.Map(entity.Order),
            Refunds = entity.Refunds?
                .Select(o => _refundMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public OrderProduct? Map(OrderProductDto? dto)
    {
        if (dto == null) return null;

        var entity = new OrderProduct()
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            OrderId = dto.OrderId,
            Quantity = dto.Quantity,
            TotalPrice = dto.TotalPrice,
            Product = _productMapper.Map(dto.Product),
            Order = _orderMapper.Map(dto.Order)
        };

        if (dto.Refunds != null)
        {
            entity.Refunds = dto.Refunds?
                .Select(o => _refundMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}