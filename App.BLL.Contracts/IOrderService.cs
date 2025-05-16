using App.BLL.DTO;
using App.DTO.V1.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IOrderService : IBaseService<OrderBllDto>
{
    // TODO - CHANGE DTO TYPE
    Task<OrderBllDto> PlaceOrderAsync(Guid personId, CreateOrderDto dto);
    
    // TODO - CHANGE DTO TYPE
    Task<IEnumerable<UserOrdersDto>> GetUsersOrdersAsync(Guid personId);
    Task<IEnumerable<PlacedOrderDto>> GetAllPlacedOrdersAsync();
    Task ChangeOrderStatusAsync(Guid orderId, string newStatus);
}