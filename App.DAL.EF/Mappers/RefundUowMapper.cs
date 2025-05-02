using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class RefundUowMapper : IUowMapper<RefundDalDto, Refund>
{
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
            OrderProduct = entity.OrderProduct == null
                ? null
                : new OrderProductDalDto()
                {
                    Id = entity.OrderProduct.Id,
                    ProductId = entity.OrderProduct.ProductId,
                    OrderId = entity.OrderProduct.OrderId,
                    Quantity = entity.OrderProduct.Quantity,
                    TotalPrice = entity.OrderProduct.TotalPrice
                },
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
            OrderProduct = dto.OrderProduct == null
                ? null
                : new OrderProduct()
                {
                    Id = dto.OrderProduct.Id,
                    ProductId = dto.OrderProduct.ProductId,
                    OrderId = dto.OrderProduct.OrderId,
                    Quantity = dto.OrderProduct.Quantity,
                    TotalPrice = dto.OrderProduct.TotalPrice
                },
        };

        return entity;
    }
}