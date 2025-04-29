using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class OrderProductRepository : BaseRepository<OrderProductDto, OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new OrderProductMapper())
    {
    }
}