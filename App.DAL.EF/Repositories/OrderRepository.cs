using App.DAL.Contracts;
using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class OrderRepository : BaseRepository<OrderDto, Order>, IOrderRepository
{
    public OrderRepository(AppDbContext repositoryDbContext, IMapper<OrderDto, Order> mapper) : base(repositoryDbContext, mapper)
    {
    }
}