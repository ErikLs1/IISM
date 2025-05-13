using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class InventoryRepository : BaseRepository<InventoryDalDto, Inventory>, IInventoryRepository
{
    public InventoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new InventoryUowMapper())
    {
    }

    // TODO MAPPING
    public async Task<Inventory?> FindByWarehouseIdAndProductIdAsync(Guid warehouseId, Guid productId)
    {
        var res = await GetQuery()
            .FirstOrDefaultAsync(
                i => i.WarehouseId == warehouseId && 
                     i.ProductId == productId);
        return res;
    }

    public async Task<IEnumerable<InventoryDalDto>> GetProductsByWarehouseIdAsync(Guid warehouseId)
    {
        var products = await GetQuery()
            .Where(i => i.WarehouseId == warehouseId)
            .Include(i => i.Product)
            .ToListAsync();

        return products.Select(x => Mapper.Map(x)!);
    }
}