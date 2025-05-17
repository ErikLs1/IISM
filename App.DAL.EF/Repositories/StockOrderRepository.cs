using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StockOrderRepository : BaseRepository<StockOrderDalDto, StockOrder>, IStockOrderRepository
{
    public StockOrderRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new StockOrderUowMapper())
    {
    }

    public async override Task<IEnumerable<StockOrderDalDto>> AllAsync(Guid userId = default)
    {
        return await GetQuery()
            .Include(s => s.Supplier)
            .Include(w => w.Warehouse)
            .Select(x => Mapper.Map(x)!)
            .ToListAsync();
    }
}