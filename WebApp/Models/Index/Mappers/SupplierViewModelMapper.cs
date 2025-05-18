using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

public class SupplierViewModelMapper
{
    public SupplierMvcDto Map(SupplierBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new SupplierMvcDto()
        {
            Id = dto.Id,
            SupplierName = dto.SupplierName,
            SupplierPhoneNumber = dto.SupplierPhoneNumber,
            SupplierEmail = dto.SupplierEmail,
            SupplierAddress = dto.SupplierAddress,
            SupplierStreet = dto.SupplierStreet,
            SupplierCity = dto.SupplierCity,
            SupplierState = dto.SupplierState,
            SupplierCountry = dto.SupplierCountry,
            SupplierPostalCode = dto.SupplierPostalCode
        };
    }
    
    public SupplierBllDto Map(SupplierMvcDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new SupplierBllDto()
        {
            Id = dto.Id,
            SupplierName = dto.SupplierName,
            SupplierPhoneNumber = dto.SupplierPhoneNumber,
            SupplierEmail = dto.SupplierEmail,
            SupplierAddress = dto.SupplierAddress,
            SupplierStreet = dto.SupplierStreet,
            SupplierCity = dto.SupplierCity,
            SupplierState = dto.SupplierState,
            SupplierCountry = dto.SupplierCountry,
            SupplierPostalCode = dto.SupplierPostalCode
        };
    }
}