using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IInventoryRepository : IBaseRepository<InventoryDalDto>
{
    Task<Inventory?> FindByWarehouseIdAndProductIdAsync(Guid warehouseId, Guid productId);
    Task<IEnumerable<InventoryDalDto>> GetProductsByWarehouseIdAsync(Guid warehouseId);
}