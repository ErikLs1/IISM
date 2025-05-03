using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class SupplierService : BaseService<SupplierBllDto, SupplierDalDto, ISupplierRepository>, ISupplierService
{
    public SupplierService(
        IAppUow serviceUow, 
        IBllMapper<SupplierBllDto, SupplierDalDto> bllMapper) : base(serviceUow, serviceUow.SupplierRepository, bllMapper)
    {
    }
}