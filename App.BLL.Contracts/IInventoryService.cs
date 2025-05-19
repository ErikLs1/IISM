using App.BLL.DTO;
using Base.BLL.Contracts;
using Base.Helpers;

namespace App.BLL.Contracts;

public interface IInventoryService : IBaseService<InventoryBllDto>
{
    Task<IEnumerable<InventoryBllDto>> GetProductsByWarehouseIdAsync(Guid warehouseId);
    Task<IEnumerable<InventoryProductsBllDto>> GetAllInventoryProductsAsync();
    Task<PagedData<InventoryProductsBllDto>> GetPagedDataAsync(
        int pageIndex, int pageSize,
        decimal? minPrice, decimal? maxPrice, string? category, string? name);
}