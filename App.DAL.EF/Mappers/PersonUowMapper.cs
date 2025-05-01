using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonUowMapper : IUowMapper<PersonDalDto, Person>
{
    private readonly OrderUowMapper _orderUowMapper;

    public PersonUowMapper(OrderUowMapper orderUowMapper)
    {
        _orderUowMapper = orderUowMapper;
    }

    public PersonDalDto? Map(Person? entity)
    {
        if (entity == null) return null;

        var dto = new PersonDalDto()
        {
            Id = entity.Id,
            PersonFirstName = entity.PersonFirstName,
            PersonLastName = entity.PersonLastName,
            PersonPhoneNumber = entity.PersonPhoneNumber,
            PersonAddress = entity.PersonAddress,
            PersonGender = entity.PersonGender,
            PersonDateOfBirth = entity.PersonDateOfBirth,
            Orders = entity.Orders?
                         .Select(o => _orderUowMapper.Map(o)!)
                         .ToList(),
        };

        return dto;
    }

    public Person? Map(PersonDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Person()
        {
            Id = dto.Id,
            PersonFirstName = dto.PersonFirstName,
            PersonLastName = dto.PersonLastName,
            PersonPhoneNumber = dto.PersonPhoneNumber,
            PersonAddress = dto.PersonAddress,
            PersonGender = dto.PersonGender,
            PersonDateOfBirth = dto.PersonDateOfBirth,
        };

        if (dto.Orders != null)
        {
            entity.Orders = dto.Orders
                .Select(o => _orderUowMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}