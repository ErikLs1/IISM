using App.BLL.DTO;
using App.DTO.V1.DTO;

namespace App.DTO.V1.Mappers;

public class UserOrdersMapper
{
    public UserOrdersDto? Map(UserOrdersBllDto? entity)
    {
        if (entity == null) return null;

        var dto = new UserOrdersDto
        {
            OrderTotalPrice = entity.OrderTotalPrice,
            OrderShippingAddress = entity.OrderShippingAddress,
            OrderStatus = entity.OrderStatus,
            Products = entity.Products!.Select(op => new OrderProductDto
            {
                Quantity = op.Quantity,
                OrderProductPrice = op.TotalPrice,
                ProductName = op.Product!.ProductName,
                ProductDescription = op.Product!.ProductDescription
            })
        };

        return dto;
    }
}