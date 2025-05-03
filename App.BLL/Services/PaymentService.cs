using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class PaymentService : BaseService<PaymentBllDto, PaymentDalDto, IPaymentRepository>, IPaymentService
{
    public PaymentService(
        IAppUow serviceUow, 
        IBllMapper<PaymentBllDto, PaymentDalDto> bllMapper) : base(serviceUow, serviceUow.PaymentRepository, bllMapper)
    {
    }
}