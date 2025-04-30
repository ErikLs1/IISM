using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class RefundMapper : IMapper<RefundDto, Refund>
{
    private readonly OrderProductMapper _orderProductMapper;

    public RefundMapper(OrderProductMapper orderProductMapper)
    {
        _orderProductMapper = orderProductMapper;
    }

    public RefundDto? Map(Refund? entity)
    {
        if (entity == null) return null;

        var dto = new RefundDto()
        {
            Id = entity.Id,
            OrderProductId = entity.OrderProductId,
            RefundAmount = entity.RefundAmount,
            RefundReason = entity.RefundReason,
            RefundStatus = entity.RefundStatus,
            OrderProduct = _orderProductMapper.Map(entity.OrderProduct),
        };

        return dto;
    }

    public Refund? Map(RefundDto? dto)
    {
        if (dto == null) return null;

        var entity = new Refund()
        {
            Id = dto.Id,
            OrderProductId = dto.OrderProductId,
            RefundAmount = dto.RefundAmount,
            RefundReason = dto.RefundReason,
            RefundStatus = dto.RefundStatus,
            OrderProduct = _orderProductMapper.Map(dto.OrderProduct),
        };

        return entity;
    }
}