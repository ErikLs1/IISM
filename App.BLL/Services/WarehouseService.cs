using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class WarehouseService : BaseService<WarehouseBllDto, WarehouseDalDto, IWarehouseRepository>, IWarehouseService
{
    public WarehouseService(
        IAppUow serviceUow, 
        IMapper<WarehouseBllDto, WarehouseDalDto> mapper) : base(serviceUow, serviceUow.WarehouseRepository, mapper)
    {
    }

    public async Task<WarehouseFiltersBllDto> GetWarehouseFiltersAsync()
    {
        var filters = await ServiceRepository.GetDistinctFiltersAsync();
        return new WarehouseFiltersBllDto()
        {
            Streets = filters.Streets,
            Cities = filters.Cities,
            States = filters.States,
            Countries = filters.Countries
        };
    }

    public async Task<IEnumerable<WarehouseBllDto>> GetFilteredWarehousesAsync(string? street, string? city, string? state, string? country)
    {
        var warehouses = await ServiceRepository.GetFilteredWarehousesAsync(street, city, state, country);
        return warehouses.Select(x => Mapper.Map(x)!);
    }
}