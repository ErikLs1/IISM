using App.BLL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IWarehouseService : IBaseService<WarehouseBllDto>
{
    Task<WarehouseFiltersBllDto> GetWarehouseFiltersAsync();

    Task<IEnumerable<WarehouseBllDto>> GetFilteredWarehousesAsync(
        string? street,
        string? city,
        string? state,
        string? country
    );
}