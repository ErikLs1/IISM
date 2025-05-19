using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IInventoryRepository : IBaseRepository<InventoryDalDto>
{
    Task<Inventory?> FindByWarehouseIdAndProductIdAsync(Guid warehouseId, Guid productId);
    Task<IEnumerable<InventoryDalDto>> GetProductsByWarehouseIdAsync(Guid warehouseId);

    Task<IEnumerable<InventoryProductsDalDto>> GetAllInventoryProductsAsync();

    Task<IEnumerable<InventoryProductsDalDto>> GetFilteredInventoryProductsAsync(
        decimal? minPrice,
        decimal? maxPrice,
        string? category,
        string? productName
    );

    Task<int> GetProductQuantityOnWarehouseByWarehouseIdAsync(Guid warehouseId);
}