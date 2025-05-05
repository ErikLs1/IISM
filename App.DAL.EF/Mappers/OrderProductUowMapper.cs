using App.DAL.DTO;
using App.Domain;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class OrderProductUowMapper : IMapper<OrderProductDalDto, OrderProduct>
{
    public OrderProductDalDto? Map(OrderProduct? entity)
    {
        if (entity == null) return null;

        var dto = new OrderProductDalDto()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            OrderId = entity.OrderId,
            Quantity = entity.Quantity,
            TotalPrice = entity.TotalPrice,
            Product = entity.Product == null
                ? null
                : new ProductDalDto()
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
                : new OrderDalDto()
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
                    .Select(o => new RefundDalDto()
                    {
                        OrderProductId = o.OrderProductId,
                        RefundAmount = o.RefundAmount,
                        RefundReason = o.RefundReason,
                        RefundStatus = o.RefundStatus
                    }).ToList()
        };

        return dto;
    }

    public OrderProduct? Map(OrderProductDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new OrderProduct()
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            OrderId = dto.OrderId,
            Quantity = dto.Quantity,
            TotalPrice = dto.TotalPrice,
            Product = dto.Product == null
                ? null
                : new Product()
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
                : new Order()
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
                    .Select(o => new Refund()
                    {
                        OrderProductId = o.OrderProductId,
                        RefundAmount = o.RefundAmount,
                        RefundReason = o.RefundReason,
                        RefundStatus = o.RefundStatus
                    }).ToList();
        }

        return entity;
    }
}