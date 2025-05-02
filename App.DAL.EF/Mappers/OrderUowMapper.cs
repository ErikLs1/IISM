using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class OrderUowMapper : IUowMapper<OrderDalDto, Order>
{
    public OrderDalDto? Map(Order? entity)
    {
        if (entity == null) return null;

        var dto = new OrderDalDto()
        {
            Id = entity.Id,
            PersonId = entity.PersonId,
            OrderShippingAddress = entity.OrderShippingAddress,
            OrderStatus = entity.OrderStatus,
            OrderTotalPrice = entity.OrderTotalPrice,
            Person = entity.Person == null
                ? null
                : new PersonDalDto()
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
                    .Select(o => new OrderProductDalDto()
                    {
                        ProductId = o.ProductId,
                        OrderId = o.ProductId,
                        Quantity = o.Quantity,
                        TotalPrice = o.TotalPrice
                    }).ToList(),
            Payments = entity.Payments == null
                ? []
                : entity.Payments
                    .Select(o => new PaymentDalDto()
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

    public Order? Map(OrderDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Order()
        {
            Id = dto.Id,
            PersonId = dto.PersonId,
            OrderShippingAddress = dto.OrderShippingAddress,
            OrderStatus = dto.OrderStatus,
            OrderTotalPrice = dto.OrderTotalPrice,
            Person = dto.Person == null
                ? null
                : new Person()
                {
                    Id = dto.Person.Id
                },
        };

        if (dto.OrderProducts != null)
        {
            entity.OrderProducts = dto.OrderProducts == null
                ? []
                : dto.OrderProducts
                    .Select(o => new OrderProduct()
                    {
                        Id = o.Id
                    }).ToList();
        }

        if (dto.Payments != null)
        {
            entity.Payments = dto.Payments == null
                ? []
                : dto.Payments
                    .Select(o => new Payment()
                    {
                        Id = o.Id
                    }).ToList();
        }

        return entity;
    }
}