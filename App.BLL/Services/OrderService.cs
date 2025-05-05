using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class OrderService : BaseService<OrderBllDto, OrderDalDto, IOrderRepository>, IOrderService
{
    public OrderService(
        IAppUow serviceUow, 
        IMapper<OrderBllDto, OrderDalDto> mapper) : base(serviceUow, serviceUow.OrderRepository, mapper)
    {
    }
}