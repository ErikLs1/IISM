using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class StockOrderRepository : BaseRepository<StockOrder>, IStockOrderRepository
{
    public StockOrderRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}