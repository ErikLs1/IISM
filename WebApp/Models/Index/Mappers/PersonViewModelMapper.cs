using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

/// <summary>
/// 
/// </summary>
public class PersonViewModelMapper
{
    public PersonMvcDto Map(PersonBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new PersonMvcDto()
        {
            Id = dto.Id,
            PersonFirstName = dto.PersonFirstName,
            PersonLastName = dto.PersonLastName,
            PersonPhoneNumber = dto.PersonPhoneNumber,
            PersonAddress = dto.PersonAddress,
            PersonGender = dto.PersonGender,
            PersonDateOfBirth = dto.PersonDateOfBirth
        };
    }
}