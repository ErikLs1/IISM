using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class StockOrderItemService : BaseService<StockOrderItemBllDto, StockOrderItemDalDto, IStockOrderItemRepository>, IStockOrderItemService
{
    public StockOrderItemService(
        IAppUow serviceUow, 
        IMapper<StockOrderItemBllDto, StockOrderItemDalDto> mapper) : base(serviceUow, serviceUow.StockOrderItemRepository, mapper)
    {
    }

    public async override Task<IEnumerable<StockOrderItemBllDto>> AllAsync(Guid userId = default)
    {
        var entities = await ServiceRepository.AllAsync(userId);
        return entities.Select(e => Mapper.Map(e)!).ToList();
    }
}