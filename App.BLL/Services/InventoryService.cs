using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class InventoryService : BaseService<InventoryBllDto, InventoryDalDto>, IInventoryService
{
    public InventoryService(
        IBaseUow serviceUow, 
        IBaseRepository<InventoryDalDto, Guid> serviceRepository,
        IBllMapper<InventoryBllDto, InventoryDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}