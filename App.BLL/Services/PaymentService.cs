using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class PaymentService : BaseService<PaymentBllDto, PaymentDalDto>, IPaymentService
{
    public PaymentService(
        IBaseUow serviceUow, 
        IBaseRepository<PaymentDalDto, Guid> serviceRepository, 
        IBllMapper<PaymentBllDto, PaymentDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}