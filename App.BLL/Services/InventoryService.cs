using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class InventoryService : BaseService<InventoryBllDto, InventoryDalDto, IInventoryRepository>, IInventoryService
{
    public InventoryService(
        IAppUow serviceUow, 
        IBllMapper<InventoryBllDto, InventoryDalDto> bllMapper) : base(serviceUow, serviceUow.InventoryRepository, bllMapper)
    {
    }
}