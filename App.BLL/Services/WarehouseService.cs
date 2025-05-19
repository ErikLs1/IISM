using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class WarehouseService : BaseService<WarehouseBllDto, WarehouseDalDto, IWarehouseRepository>, IWarehouseService
{
    private readonly IAppUow _uow;
    public WarehouseService(
        IAppUow uow, 
        IMapper<WarehouseBllDto, WarehouseDalDto> mapper) : base(uow, uow.WarehouseRepository, mapper)
    {
        _uow = uow;
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
        var dto =  warehouses.Select(x => Mapper.Map(x)!).ToList();
        foreach (var w in dto)
        {
            var used = await _uow.InventoryRepository.GetProductQuantityOnWarehouseByWarehouseIdAsync(w.Id);
            w.WarehouseAvailableCapacity = w.WarehouseCapacity - used;
        }

        return dto;
    }

    public override async Task<IEnumerable<WarehouseBllDto>> AllAsync(Guid userId = default)
    {
        var data = (await base.AllAsync(userId)).ToList();
        foreach (var w in data)
        {
            var used = await _uow.InventoryRepository.GetProductQuantityOnWarehouseByWarehouseIdAsync(w.Id);
            w.WarehouseAvailableCapacity = w.WarehouseCapacity - used;
        }

        return data;
    }
}