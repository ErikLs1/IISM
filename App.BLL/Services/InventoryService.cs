using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class InventoryService : BaseService<InventoryBllDto, InventoryDalDto, IInventoryRepository>, IInventoryService
{
    public InventoryService(
        IAppUow serviceUow, 
        IMapper<InventoryBllDto, InventoryDalDto> mapper) : base(serviceUow, serviceUow.InventoryRepository, mapper)
    {
    }

    public async Task<IEnumerable<InventoryBllDto>> GetProductsByWarehouseIdAsync(Guid warehouseId)
    {
        var dal = await ServiceRepository.GetProductsByWarehouseIdAsync(warehouseId);
        return dal.Select(x => Mapper.Map(x)!);
    }
}