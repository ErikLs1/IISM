using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IWarehouseRepository : IBaseRepository<WarehouseDalDto>
{
    Task<WarehouseFiltersDalDto> GetDistinctFiltersAsync();
    Task<IEnumerable<WarehouseDalDto>> GetFilteredWarehousesAsync(
        string? street,
        string? city,
        string? state,
        string? country);
}