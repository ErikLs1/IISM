using App.DAL.DTO;
using App.Domain;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PaymentUowMapper : IMapper<PaymentDalDto, Payment>
{
    public PaymentDalDto? Map(Payment? entity)
    {
        if (entity == null) return null;

        var dto = new PaymentDalDto()
        {
            Id = entity.Id,
            OrderId = entity.OrderId,
            PaymentMethod = entity.PaymentMethod,
            PaymentStatus = entity.PaymentStatus,
            PaymentAmount = entity.PaymentAmount,
            PaymentDate = entity.PaymentDate,
            Order = entity.Order == null
                ? null
                : new OrderDalDto()
                {
                    Id = entity.Order.Id,
                    PersonId = entity.Order.Id,
                    OrderShippingAddress = entity.Order.OrderShippingAddress,
                    OrderStatus = entity.Order.OrderStatus,
                    OrderTotalPrice = entity.Order.OrderTotalPrice
                },
        };

        return dto;
    }

    public Payment? Map(PaymentDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Payment()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            PaymentMethod = dto.PaymentMethod,
            PaymentStatus = dto.PaymentStatus,
            PaymentAmount = dto.PaymentAmount,
            PaymentDate = dto.PaymentDate,
            Order = dto.Order == null
                ? null
                : new Order()
                {
                    Id = dto.Order.Id,
                    PersonId = dto.Order.Id,
                    OrderShippingAddress = dto.Order.OrderShippingAddress,
                    OrderStatus = dto.Order.OrderStatus,
                    OrderTotalPrice = dto.Order.OrderTotalPrice
                },
        };

        return entity;
    }
}