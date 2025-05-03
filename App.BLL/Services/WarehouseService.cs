using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class WarehouseService : BaseService<WarehouseBllDto, WarehouseDalDto, IWarehouseRepository>, IWarehouseService
{
    public WarehouseService(
        IAppUow serviceUow, 
        IBllMapper<WarehouseBllDto, WarehouseDalDto> bllMapper) : base(serviceUow, serviceUow.WarehouseRepository, bllMapper)
    {
    }
}