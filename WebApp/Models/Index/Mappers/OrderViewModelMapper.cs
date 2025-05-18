using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

public class OrderViewModelMapper
{
    public OrderMvcDto Map(OrderBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new OrderMvcDto()
        {
            Id = dto.Id,
            OrderShippingAddress = dto.OrderShippingAddress,
            OrderStatus = dto.OrderStatus,
            OrderTotalPrice = dto.OrderTotalPrice
        };
    }
    
    /*public OrderBllDto Map(OrderMvcDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new OrderBllDto()
        {
            Id = dto.Id,
            OrderShippingAddress = dto.OrderShippingAddress,
            OrderStatus = dto.OrderStatus,
            OrderTotalPrice = dto.OrderTotalPrice
        };
    }*/
}