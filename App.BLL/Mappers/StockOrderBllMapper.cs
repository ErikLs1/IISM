using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class StockOrderBllMapper : IBllMapper<StockOrderBllDto, StockOrderDalDto>
{
    public StockOrderDalDto? Map(StockOrderBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public StockOrderBllDto? Map(StockOrderDalDto? entity)
    {
        throw new NotImplementedException();
    }
}