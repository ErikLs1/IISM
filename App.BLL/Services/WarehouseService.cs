using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class WarehouseService : BaseService<WarehouseBllDto, WarehouseDalDto>, IWarehouseService
{
    public WarehouseService(
        IBaseUow serviceUow, 
        IBaseRepository<WarehouseDalDto, Guid> serviceRepository, 
        IBllMapper<WarehouseBllDto, WarehouseDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}