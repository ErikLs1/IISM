using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class StockOrderService : BaseService<StockOrderBllDto, StockOrderDalDto, IStockOrderRepository>, IStockOrderService
{
    public StockOrderService(
        IAppUow serviceUow, 
        IMapper<StockOrderBllDto, StockOrderDalDto> mapper) : base(serviceUow, serviceUow.StockOrderRepository, mapper)
    {
    }
}