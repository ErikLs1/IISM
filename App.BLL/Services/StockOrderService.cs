using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class StockOrderService : BaseService<StockOrderBllDto, StockOrderDalDto>, IStockOrderService
{
    public StockOrderService(
        IBaseUow serviceUow, 
        IBaseRepository<StockOrderDalDto, Guid> serviceRepository, 
        IBllMapper<StockOrderBllDto, StockOrderDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}