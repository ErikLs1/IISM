using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class OrderProductMapper : IMapper<OrderProductDto, OrderProduct>
{
    public OrderProductDto? Map(OrderProduct? entity)
    {
        throw new NotImplementedException();
    }

    public OrderProduct? Map(OrderProductDto? entity)
    {
        throw new NotImplementedException();
    }
}