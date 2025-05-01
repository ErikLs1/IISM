using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class ProductService : BaseService<ProductBllDto, ProductDalDto>, IProductService
{
    public ProductService(
        IBaseUow serviceUow, 
        IBaseRepository<ProductDalDto, Guid> serviceRepository, 
        IBllMapper<ProductBllDto, ProductDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}