using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IOrderRepository : IBaseRepository<OrderDalDto>
{
    Task<List<OrderDalDto>> GetOrdersByPersonIdAsync(Guid personId);
    Task<List<OrderDalDto>> GetAllPlacedOrdersAsync();
    Task UpdateOrderStatus(Guid orderId, string orderStatus);
}