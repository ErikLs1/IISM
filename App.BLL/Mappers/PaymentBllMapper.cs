using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class PaymentBllMapper : IBllMapper<PaymentBllDto, PaymentDalDto>
{
    public PaymentDalDto? Map(PaymentBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public PaymentBllDto? Map(PaymentDalDto? entity)
    {
        throw new NotImplementedException();
    }
}