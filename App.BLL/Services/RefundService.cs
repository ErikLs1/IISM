using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class RefundService : BaseService<RefundBllDto, RefundDalDto>, IRefundService
{
    public RefundService(
        IBaseUow serviceUow, 
        IBaseRepository<RefundDalDto, Guid> serviceRepository, 
        IBllMapper<RefundBllDto, RefundDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}