using App.BLL.DTO;
using App.DAL.DTO;

namespace App.BLL.Mappers;

public class PlacedOrderBllMapper
{
    public PlacedOrderBllDto? Map(OrderDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new PlacedOrderBllDto
        {
            OrderId = entity.Id,
            CustomerFirstName = entity.Person!.PersonFirstName,
            CustomerLastName = entity.Person!.PersonLastName,
            TotalNumberOfProducts = entity.OrderProducts!.Sum(op => op.Quantity),
            OrderedAt = entity.CreatedAt,
            OrderStatus = entity.OrderStatus,
            Products = entity.OrderProducts == null
                ? new List<OrderProductBllDto>()
                : entity.OrderProducts
                    .Select(op => new OrderProductBllDto
                    {
                        Quantity = op.Quantity,
                        TotalPrice = op.TotalPrice,
                        ProductName = op.Product?.ProductName!,
                        ProductDescription = op.Product?.ProductDescription!,
                    })
                    .ToList()
        };

        return dto;
    }
}