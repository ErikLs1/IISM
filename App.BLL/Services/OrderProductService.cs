using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class OrderProductService : BaseService<OrderProductBllDto, OrderProductDalDto>, IOrderProductService
{
    public OrderProductService(
        IBaseUow serviceUow, 
        IBaseRepository<OrderProductDalDto, Guid> serviceRepository, 
        IBllMapper<OrderProductBllDto, OrderProductDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}