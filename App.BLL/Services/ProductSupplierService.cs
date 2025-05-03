using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class ProductSupplierService : BaseService<ProductSupplierBllDto, ProductSupplierDalDto, IProductSupplierRepository>, IProductSupplierService
{
    public ProductSupplierService(
        IAppUow serviceUow, 
        IBllMapper<ProductSupplierBllDto, ProductSupplierDalDto> bllMapper) : base(serviceUow, serviceUow.ProductSupplierRepository, bllMapper)
    {
    }
}