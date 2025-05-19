using App.BLL.DTO;
using App.DTO.V1.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IOrderService : IBaseService<OrderBllDto>
{
    Task<OrderBllDto> PlaceOrderAsync(Guid personId, CreateOrderBllDto dto);
    Task<IEnumerable<UserOrdersBllDto>> GetUsersOrdersAsync(Guid personId);
    Task<IEnumerable<PlacedOrderBllDto>> GetAllPlacedOrdersAsync();
    Task ChangeOrderStatusAsync(Guid orderId, string newStatus);
}