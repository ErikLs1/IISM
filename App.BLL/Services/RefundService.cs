using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class RefundService : BaseService<RefundBllDto, RefundDalDto, IRefundRepository>, IRefundService
{
    public RefundService(
        IAppUow serviceUow, 
        IMapper<RefundBllDto, RefundDalDto> mapper) : base(serviceUow, serviceUow.RefundRepository, mapper)
    {
    }
}