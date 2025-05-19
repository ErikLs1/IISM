using App.BLL.DTO;
using App.DAL.DTO;

namespace App.BLL.Mappers;

public class UserOrdersBllMapper
{
    public UserOrdersBllDto? Map(OrderDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new UserOrdersBllDto
        {
            OrderTotalPrice = entity.OrderTotalPrice,
            OrderShippingAddress = entity.OrderShippingAddress,
            OrderStatus = entity.OrderStatus,
            Products = entity.OrderProducts!.Select(op => new OrderProductBllDto
            {
                Quantity = op.Quantity,
                // OrderProductPrice = op.TotalPrice,
                TotalPrice = op.TotalPrice,
                ProductName = op.Product!.ProductName,
                ProductDescription = op.Product!.ProductDescription
            })
        };

        return dto;
    }
}