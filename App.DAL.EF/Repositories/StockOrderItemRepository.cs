using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StockOrderItemRepository : BaseRepository<StockOrderItemDalDto, StockOrderItem>, IStockOrderItemRepository
{
    public StockOrderItemRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new StockOrderItemUowMapper())
    {
    }

    public async override Task<IEnumerable<StockOrderItemDalDto>> AllAsync(Guid userId = default)
    {
        return await GetQuery()
            .Include(p => p.Product)
            .Select(x => Mapper.Map(x)!)
            .ToListAsync();
    }
}