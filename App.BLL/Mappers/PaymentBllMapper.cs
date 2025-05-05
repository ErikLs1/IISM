using App.BLL.DTO;
using App.DAL.DTO;
using Base.Contracts;

namespace App.BLL.Mappers;

public class PaymentBllMapper : IMapper<PaymentBllDto, PaymentDalDto>
{
    public PaymentBllDto? Map(PaymentDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new PaymentBllDto()
        {
            Id = entity.Id,
            OrderId = entity.OrderId,
            PaymentMethod = entity.PaymentMethod,
            PaymentStatus = entity.PaymentStatus,
            PaymentAmount = entity.PaymentAmount,
            PaymentDate = entity.PaymentDate,
            Order = entity.Order == null
                ? null
                : new OrderBllDto()
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

    public PaymentDalDto? Map(PaymentBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new PaymentDalDto()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            PaymentMethod = dto.PaymentMethod,
            PaymentStatus = dto.PaymentStatus,
            PaymentAmount = dto.PaymentAmount,
            PaymentDate = dto.PaymentDate,
            Order = dto.Order == null
                ? null
                : new OrderDalDto()
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