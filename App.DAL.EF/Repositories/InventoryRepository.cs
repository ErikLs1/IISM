using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Helpers;
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

    public async  Task<PagedData<InventoryProductsDalDto>> GetPagedDataAsync(
        int pageIndex, int pageSize,
        decimal? minPrice, decimal? maxPrice, string? category, string? name)
    {
        IQueryable<Inventory> query = GetQuery()
            .Include(i => i.Product).ThenInclude(i => i!.Category)
            .Include(i => i.Warehouse);
        
        if (minPrice != null)
            query = query.Where(x => x.Product!.ProductPrice * 1.5m >= minPrice.Value);

        if (maxPrice != null)
            query = query.Where(x => x.Product!.ProductPrice * 1.5m <= maxPrice.Value);
        
        if (!string.IsNullOrEmpty(category))
            query = query.Where(x => x.Product!.Category!.CategoryName == category);
        
        if (!string.IsNullOrEmpty(name))
        {
            var lower = name.ToLower();
            query = query.Where(i =>
                i.Product!.ProductName
                    .ToLower()
                    .Contains(lower)
            );
        }
        
        var totalCount = await query.CountAsync();

        var pageEntities = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var dalDto = pageEntities
            .Select(e => _mapper.Map(e)!)  
            .ToList();
        
        return new PagedData<InventoryProductsDalDto>
        {
            Items = dalDto,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    public async Task<int> GetProductQuantityOnWarehouseByWarehouseIdAsync(Guid warehouseId)
    {
        return await GetQuery()
            .Where(w => w.WarehouseId == warehouseId)
            .SumAsync(i => i.Quantity);
    }

    public async override Task<IEnumerable<InventoryDalDto>> AllAsync(Guid userId = default)
    {
        return await GetQuery(userId)
                .Include(i => i.Product)
                .Include(i => i.Warehouse)
            .Select(e => Mapper.Map(e)!).ToListAsync();
    }
}