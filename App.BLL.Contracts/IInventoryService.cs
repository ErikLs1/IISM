using App.BLL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IInventoryService : IBaseService<InventoryBllDto>
{
    Task<IEnumerable<InventoryBllDto>> GetProductsByWarehouseIdAsync(Guid warehouseId);
    Task<IEnumerable<InventoryProductsBllDto>> GetAllInventoryProductsAsync();
    Task<IEnumerable<InventoryProductsBllDto>> GetFilteredInventoryProductsAsync(
        decimal? minPrice, decimal? maxPrice, string? category, string? name);
}