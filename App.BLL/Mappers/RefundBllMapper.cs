using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class RefundBllMapper : IBllMapper<RefundBllDto, RefundDalDto>
{
    public RefundDalDto? Map(RefundBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new RefundDalDto()
        {
            Id = dto.Id,
            OrderProductId = dto.OrderProductId,
            RefundAmount = dto.RefundAmount,
            RefundReason = dto.RefundReason,
            RefundStatus = dto.RefundStatus,
            OrderProduct = dto.OrderProduct == null
                ? null
                : new OrderProductDalDto()
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

    public RefundBllDto? Map(RefundDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new RefundBllDto()
        {
            Id = entity.Id,
            OrderProductId = entity.OrderProductId,
            RefundAmount = entity.RefundAmount,
            RefundReason = entity.RefundReason,
            RefundStatus = entity.RefundStatus,
            OrderProduct = entity.OrderProduct == null
                ? null
                : new OrderProductBllDto()
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
}