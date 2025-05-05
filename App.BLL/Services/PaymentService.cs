using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class PaymentService : BaseService<PaymentBllDto, PaymentDalDto, IPaymentRepository>, IPaymentService
{
    public PaymentService(
        IAppUow serviceUow, 
        IMapper<PaymentBllDto, PaymentDalDto> mapper) : base(serviceUow, serviceUow.PaymentRepository, mapper)
    {
    }
}