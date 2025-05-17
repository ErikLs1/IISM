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

    public async Task<IEnumerable<InventoryProductsDalDto>> GetAllInventoryProductsAsync()
    {
        var product = await GetQuery()
            .Include(i => i.Product)
            .ThenInclude(i => i!.Category)
            .Include(i => i.Warehouse)
            .ToListAsync();

        return product.Select(e => new InventoryProductsDalDto()
        {
            WarehouseId = e.WarehouseId,
            ProductId = e.ProductId,
            ProductName = e.Product!.ProductName,
            CategoryName = e.Product!.Category!.CategoryName,
            ProductPrice = e.Product!.ProductPrice,
            WarehouseCity = e.Warehouse!.WarehouseCity,
            WarehouseState = e.Warehouse!.WarehouseState,
            WarehouseCountry = e.Warehouse!.WarehouseCountry,
            ProductDescription = e.Product!.ProductDescription,
        }).ToList();
    }

    public async override Task<IEnumerable<InventoryDalDto>> AllAsync(Guid userId = default)
    {
        return await GetQuery(userId)
                .Include(i => i.Product)
                .Include(i => i.Warehouse)
            .Select(e => Mapper.Map(e)!).ToListAsync();
    }
}