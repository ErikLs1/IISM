using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class WarehouseService : BaseService<WarehouseBllDto, WarehouseDalDto, IWarehouseRepository>, IWarehouseService
{
    public WarehouseService(
        IAppUow serviceUow, 
        IMapper<WarehouseBllDto, WarehouseDalDto> mapper) : base(serviceUow, serviceUow.WarehouseRepository, mapper)
    {
    }
}