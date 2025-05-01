using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PaymentUowMapper : IUowMapper<PaymentDalDto, Payment>
{
    private readonly OrderUowMapper _orderUowMapper;

    public PaymentUowMapper(OrderUowMapper orderUowMapper)
    {
        _orderUowMapper = orderUowMapper;
    }

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
            Order = _orderUowMapper.Map(entity.Order)
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
            Order = _orderUowMapper.Map(dto.Order)
        };

        return entity;
    }
}