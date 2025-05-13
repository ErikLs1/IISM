using App.BLL.DTO;
using App.DTO.V1.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IStockOrderService : IBaseService<StockOrderBllDto>
{
    Task<StockOrderBllDto> PlaceStockOrderAsync(CreateStockOrderDto dto);
}