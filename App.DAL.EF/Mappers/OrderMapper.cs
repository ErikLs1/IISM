using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class OrderMapper : IMapper<OrderDto, Order>
{
    public OrderDto? Map(Order? entity)
    {
        throw new NotImplementedException();
    }

    public Order? Map(OrderDto? entity)
    {
        throw new NotImplementedException();
    }
}