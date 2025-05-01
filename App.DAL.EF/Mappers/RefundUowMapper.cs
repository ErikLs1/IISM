using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class RefundUowMapper : IUowMapper<RefundDalDto, Refund>
{
    private readonly OrderProductUowMapper _orderProductUowMapper;

    public RefundUowMapper(OrderProductUowMapper orderProductUowMapper)
    {
        _orderProductUowMapper = orderProductUowMapper;
    }

    public RefundDalDto? Map(Refund? entity)
    {
        if (entity == null) return null;

        var dto = new RefundDalDto()
        {
            Id = entity.Id,
            OrderProductId = entity.OrderProductId,
            RefundAmount = entity.RefundAmount,
            RefundReason = entity.RefundReason,
            RefundStatus = entity.RefundStatus,
            OrderProduct = _orderProductUowMapper.Map(entity.OrderProduct),
        };

        return dto;
    }

    public Refund? Map(RefundDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Refund()
        {
            Id = dto.Id,
            OrderProductId = dto.OrderProductId,
            RefundAmount = dto.RefundAmount,
            RefundReason = dto.RefundReason,
            RefundStatus = dto.RefundStatus,
            OrderProduct = _orderProductUowMapper.Map(dto.OrderProduct),
        };

        return entity;
    }
}