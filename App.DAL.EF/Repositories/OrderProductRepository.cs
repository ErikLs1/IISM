using App.DAL.Contracts;
using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class OrderProductRepository : BaseRepository<OrderProductDalDto, OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(AppDbContext repositoryDbContext, IUowMapper<OrderProductDalDto, OrderProduct> uowMapper) : base(repositoryDbContext, uowMapper)
    {
    }
}