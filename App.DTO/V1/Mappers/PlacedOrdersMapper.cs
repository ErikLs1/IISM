using App.BLL.DTO;
using App.DTO.V1.DTO;

namespace App.DTO.V1.Mappers;

public class PlacedOrdersMapper
{
    public PlacedOrderDto? Map(PlacedOrderBllDto? entity)
    {
        if (entity == null) return null;

        var dto = new PlacedOrderDto
        {
            OrderId = entity.OrderId,
            CustomerFirstName = entity.CustomerFirstName,
            CustomerLastName = entity.CustomerLastName,
            TotalNumberOfProducts = entity.TotalNumberOfProducts,
            OrderedAt = entity.OrderedAt,
            OrderStatus = entity.OrderStatus,
            Products = entity.Products.Select(op => new OrderProductDto
            {
                Quantity = op.Quantity,
                OrderProductPrice = op.TotalPrice,
                ProductName = op.ProductName,
                ProductDescription = op.ProductDescription
            })
        };

        return dto;
    }
}