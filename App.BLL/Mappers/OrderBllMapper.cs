using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class OrderBllMapper : IBllMapper<OrderBllDto, OrderDalDto>
{
    public OrderDalDto? Map(OrderBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public OrderBllDto? Map(OrderDalDto? entity)
    {
        throw new NotImplementedException();
    }
}