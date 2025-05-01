using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class StockOrderItemBllMapper : IBllMapper<StockOrderItemBllDto, StockOrderItemDalDto>
{
    public StockOrderItemDalDto? Map(StockOrderItemBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public StockOrderItemBllDto? Map(StockOrderItemDalDto? entity)
    {
        throw new NotImplementedException();
    }
}