using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;
using Base.Helpers;

namespace App.DAL.Contracts;

public interface IInventoryRepository : IBaseRepository<InventoryDalDto>
{
    Task<Inventory?> FindByWarehouseIdAndProductIdAsync(Guid warehouseId, Guid productId);
    Task<IEnumerable<InventoryDalDto>> GetProductsByWarehouseIdAsync(Guid warehouseId);

    Task<IEnumerable<InventoryProductsDalDto>> GetAllInventoryProductsAsync();

    Task<PagedData<InventoryProductsDalDto>> GetPagedDataAsync(
        int pageIndex, int pageSize,
        decimal? minPrice, decimal? maxPrice, string? category, string? name);

    Task<int> GetProductQuantityOnWarehouseByWarehouseIdAsync(Guid warehouseId);
}