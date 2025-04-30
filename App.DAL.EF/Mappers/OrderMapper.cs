using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class OrderMapper : IMapper<OrderDto, Order>
{
    private readonly PersonMapper _personMapper;
    private readonly OrderProductMapper _orderProductMapper;
    private readonly PaymentMapper _paymentMapper;

    public OrderMapper(PersonMapper personMapper, OrderProductMapper orderProductMapper, PaymentMapper paymentMapper)
    {
        _personMapper = personMapper;
        _orderProductMapper = orderProductMapper;
        _paymentMapper = paymentMapper;
    }

    public OrderDto? Map(Order? entity)
    {
        if (entity == null) return null;

        var dto = new OrderDto()
        {
            Id = entity.Id,
            PersonId = entity.PersonId,
            OrderShippingAddress = entity.OrderShippingAddress,
            OrderStatus = entity.OrderStatus,
            OrderTotalPrice = entity.OrderTotalPrice,
            Person = _personMapper.Map(entity.Person),
            OrderProducts = entity.OrderProducts?
                .Select(o => _orderProductMapper.Map(o)!)
                .ToList(),
            Payments = entity.Payments?
                .Select(o => _paymentMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public Order? Map(OrderDto? dto)
    {
        if (dto == null) return null;

        var entity = new Order()
        {
            Id = dto.Id,
            PersonId = dto.PersonId,
            OrderShippingAddress = dto.OrderShippingAddress,
            OrderStatus = dto.OrderStatus,
            OrderTotalPrice = dto.OrderTotalPrice,
            Person = _personMapper.Map(dto.Person)
        };

        if (dto.OrderProducts != null)
        {
            entity.OrderProducts = dto.OrderProducts?
                .Select(o => _orderProductMapper.Map(o)!)
                .ToList();
        }

        if (dto.Payments != null)
        {
            entity.Payments = dto.Payments?
                .Select(o => _paymentMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}