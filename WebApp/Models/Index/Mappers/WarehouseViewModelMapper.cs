using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

public class WarehouseViewModelMapper
{
    public WarehouseMvcDto Map(WarehouseBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new WarehouseMvcDto()
        {
            Id = dto.Id,
            WarehouseAddress = dto.WarehouseAddress,
            WarehouseStreet = dto.WarehouseStreet,
            WarehouseCity = dto.WarehouseCity,
            WarehouseState = dto.WarehouseState,
            WarehouseCountry = dto.WarehouseCountry,
            WarehousePostalCode = dto.WarehousePostalCode,
            WarehouseEmail = dto.WarehouseEmail,
            WarehouseCapacity = dto.WarehouseCapacity
        };
    }
    
    public WarehouseBllDto Map(WarehouseMvcDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new WarehouseBllDto()
        {
            Id = dto.Id,
            WarehouseAddress = dto.WarehouseAddress,
            WarehouseStreet = dto.WarehouseStreet,
            WarehouseCity = dto.WarehouseCity,
            WarehouseState = dto.WarehouseState,
            WarehouseCountry = dto.WarehouseCountry,
            WarehousePostalCode = dto.WarehousePostalCode,
            WarehouseEmail = dto.WarehouseEmail,
            WarehouseCapacity = dto.WarehouseCapacity
        };
    }
}