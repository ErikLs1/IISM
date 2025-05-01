using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class OrderService : BaseService<OrderBllDto, OrderDalDto>, IOrderService
{
    public OrderService(
        IBaseUow serviceUow, 
        IBaseRepository<OrderDalDto, Guid> serviceRepository, 
        IBllMapper<OrderBllDto, OrderDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}