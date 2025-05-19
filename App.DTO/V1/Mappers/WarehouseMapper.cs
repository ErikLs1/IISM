using App.BLL.DTO;
using App.DTO.V1.DTO;
using Base.Contracts;

namespace App.DTO.V1.Mappers;

public class WarehouseMapper : IMapper<WarehouseDto, WarehouseBllDto>
{
    public WarehouseDto? Map(WarehouseBllDto? entity)
    {
        if (entity == null) return null;

        var res = new WarehouseDto()
        {
            Id = entity.Id,
            WarehouseAddress = entity.WarehouseAddress,
            WarehouseStreet = entity.WarehouseStreet,
            WarehouseCity = entity.WarehouseCity,
            WarehouseState = entity.WarehouseState,
            WarehouseCountry = entity.WarehouseCountry,
            WarehousePostalCode = entity.WarehousePostalCode,
            WarehouseEmail = entity.WarehouseEmail,
            WarehouseCapacity = entity.WarehouseCapacity,
            WarehouseAvailableCapacity = entity.WarehouseAvailableCapacity
        };

        return res;
    }

    public WarehouseBllDto? Map(WarehouseDto? entity)
    {
        if (entity == null) return null;

        var res = new WarehouseBllDto()
        {
            Id = entity.Id,
            WarehouseAddress = entity.WarehouseAddress,
            WarehouseStreet = entity.WarehouseStreet,
            WarehouseCity = entity.WarehouseCity,
            WarehouseState = entity.WarehouseState,
            WarehouseCountry = entity.WarehouseCountry,
            WarehousePostalCode = entity.WarehousePostalCode,
            WarehouseEmail = entity.WarehouseEmail,
            WarehouseCapacity = entity.WarehouseCapacity
        };

        return res;
    }
    
    /// <summary>
    /// Mapping for creating the object.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public WarehouseBllDto? Map(WarehouseCreateDto? entity)
    {
        if (entity == null) return null;

        var res = new WarehouseBllDto()
        {
            // Id = Guid.NewGuid(), // Move to service?
            WarehouseAddress = entity.WarehouseAddress,
            WarehouseStreet = entity.WarehouseStreet,
            WarehouseCity = entity.WarehouseCity,
            WarehouseState = entity.WarehouseState,
            WarehouseCountry = entity.WarehouseCountry,
            WarehousePostalCode = entity.WarehousePostalCode,
            WarehouseEmail = entity.WarehouseEmail,
            WarehouseCapacity = entity.WarehouseCapacity
        };

        return res;
    }
}