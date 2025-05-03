using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class OrderProductBllMapper : IBllMapper<OrderProductBllDto, OrderProductDalDto>
{
    public OrderProductDalDto? Map(OrderProductBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new OrderProductDalDto()
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            OrderId = dto.OrderId,
            Quantity = dto.Quantity,
            TotalPrice = dto.TotalPrice,
            Product = dto.Product == null
                ? null
                : new ProductDalDto()
                {
                    Id = dto.Product.Id,
                    CategoryId = dto.Product.CategoryId,
                    ProductName = dto.Product.ProductName,
                    ProductDescription = dto.Product.ProductDescription,
                    ProductPrice = dto.Product.ProductPrice,
                    ProductStatus = dto.Product.ProductStatus
                },
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

        if (dto.Refunds != null)
        {
            entity.Refunds = dto.Refunds == null
                ? []
                : dto.Refunds
                    .Select(o => new RefundDalDto()
                    {
                        OrderProductId = o.OrderProductId,
                        RefundAmount = o.RefundAmount,
                        RefundReason = o.RefundReason,
                        RefundStatus = o.RefundStatus
                    }).ToList();
        }

        return entity;
    }

    public OrderProductBllDto? Map(OrderProductDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new OrderProductBllDto()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            OrderId = entity.OrderId,
            Quantity = entity.Quantity,
            TotalPrice = entity.TotalPrice,
            Product = entity.Product == null
                ? null
                : new ProductBllDto()
                {
                    Id = entity.Product.Id,
                    CategoryId = entity.Product.CategoryId,
                    ProductName = entity.Product.ProductName,
                    ProductDescription = entity.Product.ProductDescription,
                    ProductPrice = entity.Product.ProductPrice,
                    ProductStatus = entity.Product.ProductStatus
                },
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
            Refunds = entity.Refunds == null
                ? []
                : entity.Refunds
                    .Select(o => new RefundBllDto()
                    {
                        OrderProductId = o.OrderProductId,
                        RefundAmount = o.RefundAmount,
                        RefundReason = o.RefundReason,
                        RefundStatus = o.RefundStatus
                    }).ToList()
        };

        return dto;
    }
}