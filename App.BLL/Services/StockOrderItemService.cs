using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class StockOrderItemService : BaseService<StockOrderItemBllDto, StockOrderItemDalDto, IStockOrderItemRepository>, IStockOrderItemService
{
    public StockOrderItemService(
        IAppUow serviceUow, 
        IBllMapper<StockOrderItemBllDto, StockOrderItemDalDto> bllMapper) : base(serviceUow, serviceUow.StockOrderItemRepository, bllMapper)
    {
    }
}