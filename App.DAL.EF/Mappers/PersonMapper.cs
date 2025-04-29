using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonMapper : IMapper<PersonDto, Person>
{
    public PersonDto? Map(Person? entity)
    {
        throw new NotImplementedException();
    }

    public Person? Map(PersonDto? entity)
    {
        throw new NotImplementedException();
    }
}