using App.BLL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IInventoryService : IBaseService<InventoryBllDto>
{
    Task<IEnumerable<InventoryBllDto>> GetProductsByWarehouseIdAsync(Guid warehouseId);
}