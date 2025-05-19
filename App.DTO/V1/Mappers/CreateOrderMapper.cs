using App.BLL.DTO;
using App.DTO.V1.DTO;

namespace App.DTO.V1.Mappers;

public class CreateOrderMapper
{
    public CreateOrderBllDto? Map(CreateOrderDto? entity)
    {
        if (entity == null) return null;

        var res = new CreateOrderBllDto()
        {
           ShippingAddress = entity.ShippingAddress,
           PaymentMethod = entity.PaymentMethod,
           Products = entity.Products
               .Select(p => new CreateOrderProductBllDto
               {
                   ProductId = p.ProductId,
                   Quantity = p.Quantity,
               })
               .ToList()
        };

        return res;
    }
}