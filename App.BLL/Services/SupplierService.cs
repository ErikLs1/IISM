using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class SupplierService : BaseService<SupplierBllDto, SupplierDalDto>, ISupplierService
{
    public SupplierService(
        IBaseUow serviceUow, 
        IBaseRepository<SupplierDalDto, Guid> serviceRepository, 
        IBllMapper<SupplierBllDto, SupplierDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}