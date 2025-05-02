using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonUowMapper : IUowMapper<PersonDalDto, Person>
{
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
            Orders = entity.Orders == null ? [] : 
                    entity.Orders
                    .Select(o => new OrderDalDto()
                         {
                             Id =o.Id,
                             PersonId = o.PersonId,
                             OrderShippingAddress = o.OrderShippingAddress,
                             OrderStatus = o.OrderStatus,
                             OrderTotalPrice = o.OrderTotalPrice,
                         }).ToList(),
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
                .Select(o => new Order()
                {
                    Id =o.Id,
                    PersonId = o.PersonId,
                    OrderShippingAddress = o.OrderShippingAddress,
                    OrderStatus = o.OrderStatus,
                    OrderTotalPrice = o.OrderTotalPrice,
                })
                .ToList();
        }

        return entity;
    }
}