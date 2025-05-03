using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class RefundService : BaseService<RefundBllDto, RefundDalDto, IRefundRepository>, IRefundService
{
    public RefundService(
        IAppUow serviceUow, 
        IBllMapper<RefundBllDto, RefundDalDto> bllMapper) : base(serviceUow, serviceUow.RefundRepository, bllMapper)
    {
    }
}