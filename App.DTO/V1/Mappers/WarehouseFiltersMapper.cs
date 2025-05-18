using App.BLL.DTO;
using App.DTO.V1.DTO;

namespace App.DTO.V1.Mappers;

public class WarehouseFiltersMapper
{
    public WarehouseFiltersDto? Map(WarehouseFiltersBllDto? entity)
    {
        if (entity == null) return null;

        var res = new WarehouseFiltersDto()
        {
            Streets = entity.Streets,
            Cities = entity.Cities,
            States = entity.States,
            Countries = entity.Countries
        };
        return res;
    }
}