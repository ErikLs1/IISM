using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class PaymentMapper : IMapper<PaymentDto, Payment>
{
    public PaymentDto? Map(Payment? entity)
    {
        throw new NotImplementedException();
    }

    public Payment? Map(PaymentDto? entity)
    {
        throw new NotImplementedException();
    }
}