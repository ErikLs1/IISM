using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class PersonBllMapper : IBllMapper<PersonBllDto, PersonDalDto>
{
    public PersonDalDto? Map(PersonBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public PersonBllDto? Map(PersonDalDto? entity)
    {
        throw new NotImplementedException();
    }
}