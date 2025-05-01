using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class StockOrderItemService : BaseService<StockOrderItemBllDto, StockOrderItemDalDto>, IStockOrderItemService
{
    public StockOrderItemService(
        IBaseUow serviceUow, 
        IBaseRepository<StockOrderItemDalDto, Guid> serviceRepository, 
        IBllMapper<StockOrderItemBllDto, StockOrderItemDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}