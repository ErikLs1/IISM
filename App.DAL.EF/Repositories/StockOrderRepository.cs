using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class StockOrderRepository : BaseRepository<StockOrderDto, StockOrder>, IStockOrderRepository
{
    public StockOrderRepository(AppDbContext repositoryDbContext, IMapper<StockOrderDto, StockOrder> mapper) : base(repositoryDbContext, mapper)
    {
    }
}