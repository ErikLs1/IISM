using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class OrderProductBllMapper : IBllMapper<OrderProductBllDto, OrderProductDalDto>
{
    public OrderProductDalDto? Map(OrderProductBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public OrderProductBllDto? Map(OrderProductDalDto? entity)
    {
        throw new NotImplementedException();
    }
}