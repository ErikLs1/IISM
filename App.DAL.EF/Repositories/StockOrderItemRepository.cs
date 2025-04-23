using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class StockOrderItemRepository : BaseRepository<StockOrderItem>, IStockOrderItemRepository
{
    public StockOrderItemRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}