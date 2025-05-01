using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class ProductSupplierService : BaseService<ProductSupplierBllDto, ProductSupplierDalDto>, IProductSupplierService
{
    public ProductSupplierService(
        IBaseUow serviceUow, 
        IBaseRepository<ProductSupplierDalDto, Guid> serviceRepository, 
        IBllMapper<ProductSupplierBllDto, ProductSupplierDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}