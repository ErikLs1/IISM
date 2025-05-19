using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class InventoryRepository : BaseRepository<InventoryDalDto, Inventory>, IInventoryRepository
{
    private readonly InventoryProductsUowMapper _mapper = new InventoryProductsUowMapper();
    public InventoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new InventoryUowMapper())
    {
    }

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

    public async Task<IEnumerable<InventoryProductsDalDto>> GetAllInventoryProductsAsync()
    {
        var products = await GetQuery()
            .Include(i => i.Product).ThenInclude(i => i!.Category)
            .Include(i => i.Warehouse)
            .Select(i => _mapper.Map(i)!)
            .ToListAsync();

        return products;
    }

    public async Task<IEnumerable<InventoryProductsDalDto>> GetFilteredInventoryProductsAsync(
        decimal? minPrice, decimal? maxPrice, string? category, string? productName)
    {
        IQueryable<Inventory> query = GetQuery();
        query = GetQuery()
            .Include(i => i.Product).ThenInclude(i => i!.Category)
            .Include(i => i.Warehouse);
        
        if (minPrice != null)
            query = query.Where(x => x.Product!.ProductPrice >= minPrice.Value);

        if (maxPrice != null)
            query = query.Where(x => x.Product!.ProductPrice <= maxPrice.Value);
        
        if (!string.IsNullOrEmpty(category))
            query = query.Where(x => x.Product!.Category!.CategoryName == category);
        
        if (!string.IsNullOrEmpty(productName))
            query = query.Where(x => x.Product!.ProductName
                .Contains(productName, StringComparison.OrdinalIgnoreCase));

        return await query.Select(i => _mapper.Map(i)!).ToListAsync();
    }

    public async override Task<IEnumerable<InventoryDalDto>> AllAsync(Guid userId = default)
    {
        return await GetQuery(userId)
                .Include(i => i.Product)
                .Include(i => i.Warehouse)
            .Select(e => Mapper.Map(e)!).ToListAsync();
    }
}