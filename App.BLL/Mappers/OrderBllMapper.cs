using App.BLL.DTO;
using App.DAL.DTO;
using Base.Contracts;

namespace App.BLL.Mappers;

public class OrderBllMapper : IMapper<OrderBllDto, OrderDalDto>
{
    public OrderBllDto? Map(OrderDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new OrderBllDto()
        {
            Id = entity.Id,
            PersonId = entity.PersonId,
            OrderShippingAddress = entity.OrderShippingAddress,
            OrderStatus = entity.OrderStatus,
            OrderTotalPrice = entity.OrderTotalPrice,
            Person = entity.Person == null
                ? null
                : new PersonBllDto()
                {
                    Id = entity.Person.Id,
                    PersonFirstName = entity.Person.PersonFirstName,
                    PersonLastName = entity.Person.PersonLastName,
                    PersonPhoneNumber = entity.Person.PersonPhoneNumber,
                    PersonAddress = entity.Person.PersonAddress,
                    PersonGender = entity.Person.PersonGender,
                    PersonDateOfBirth = entity.Person.PersonDateOfBirth,
                },
            OrderProducts = entity.OrderProducts == null
                ? []
                : entity.OrderProducts
                    .Select(o => new OrderProductBllDto()
                    {
                        ProductId = o.ProductId,
                        OrderId = o.ProductId,
                        Quantity = o.Quantity,
                        TotalPrice = o.TotalPrice
                    }).ToList(),
            Payments = entity.Payments == null
                ? []
                : entity.Payments
                    .Select(o => new PaymentBllDto()
                    {
                        OrderId = o.OrderId,
                        PaymentMethod = o.PaymentMethod,
                        PaymentStatus = o.PaymentStatus,
                        PaymentAmount = o.PaymentAmount,
                        PaymentDate = o.PaymentDate
                    }).ToList()
        };

        return dto;
    }

    public OrderDalDto? Map(OrderBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new OrderDalDto()
        {
            Id = dto.Id,
            PersonId = dto.PersonId,
            OrderShippingAddress = dto.OrderShippingAddress,
            OrderStatus = dto.OrderStatus,
            OrderTotalPrice = dto.OrderTotalPrice,
            Person = dto.Person == null
                ? null
                : new PersonDalDto()
                {
                    Id = dto.Person.Id
                },
        };

        if (dto.OrderProducts != null)
        {
            entity.OrderProducts = dto.OrderProducts == null
                ? []
                : dto.OrderProducts
                    .Select(o => new OrderProductDalDto()
                    {
                        Id = o.Id
                    }).ToList();
        }

        if (dto.Payments != null)
        {
            entity.Payments = dto.Payments == null
                ? []
                : dto.Payments
                    .Select(o => new PaymentDalDto()
                    {
                        Id = o.Id
                    }).ToList();
        }

        return entity;
    }
}