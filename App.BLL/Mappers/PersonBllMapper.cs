using App.BLL.DTO;
using App.DAL.DTO;
using Base.Contracts;

namespace App.BLL.Mappers;

public class PersonBllMapper : IMapper<PersonBllDto, PersonDalDto>
{
        public PersonBllDto? Map(PersonDalDto? entity)
        {
            if (entity == null) return null;

            var dto = new PersonBllDto()
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
                        .Select(o => new OrderBllDto()
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

        public PersonDalDto? Map(PersonBllDto? dto)
        {
            if (dto == null) return null;

            var entity = new PersonDalDto()
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
                    .Select(o => new OrderDalDto()
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