using App.DAL.Contracts;
using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class OrderRepository : BaseRepository<OrderDalDto, Order>, IOrderRepository
{
    public OrderRepository(AppDbContext repositoryDbContext, IUowMapper<OrderDalDto, Order> uowMapper) : base(repositoryDbContext, uowMapper)
    {
    }
}