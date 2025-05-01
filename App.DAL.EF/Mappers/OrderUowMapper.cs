using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class OrderUowMapper : IUowMapper<OrderDalDto, Order>
{
    private readonly PersonUowMapper _personUowMapper;
    private readonly OrderProductUowMapper _orderProductUowMapper;
    private readonly PaymentUowMapper _paymentUowMapper;

    public OrderUowMapper(PersonUowMapper personUowMapper, OrderProductUowMapper orderProductUowMapper, PaymentUowMapper paymentUowMapper)
    {
        _personUowMapper = personUowMapper;
        _orderProductUowMapper = orderProductUowMapper;
        _paymentUowMapper = paymentUowMapper;
    }

    public OrderDalDto? Map(Order? entity)
    {
        if (entity == null) return null;

        var dto = new OrderDalDto()
        {
            Id = entity.Id,
            PersonId = entity.PersonId,
            OrderShippingAddress = entity.OrderShippingAddress,
            OrderStatus = entity.OrderStatus,
            OrderTotalPrice = entity.OrderTotalPrice,
            Person = _personUowMapper.Map(entity.Person),
            OrderProducts = entity.OrderProducts?
                .Select(o => _orderProductUowMapper.Map(o)!)
                .ToList(),
            Payments = entity.Payments?
                .Select(o => _paymentUowMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public Order? Map(OrderDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Order()
        {
            Id = dto.Id,
            PersonId = dto.PersonId,
            OrderShippingAddress = dto.OrderShippingAddress,
            OrderStatus = dto.OrderStatus,
            OrderTotalPrice = dto.OrderTotalPrice,
            Person = _personUowMapper.Map(dto.Person)
        };

        if (dto.OrderProducts != null)
        {
            entity.OrderProducts = dto.OrderProducts?
                .Select(o => _orderProductUowMapper.Map(o)!)
                .ToList();
        }

        if (dto.Payments != null)
        {
            entity.Payments = dto.Payments?
                .Select(o => _paymentUowMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}