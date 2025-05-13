using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class ProductSupplierService : BaseService<ProductSupplierBllDto, ProductSupplierDalDto, IProductSupplierRepository>, IProductSupplierService
{
    public ProductSupplierService(
        IAppUow serviceUow, 
        IMapper<ProductSupplierBllDto, ProductSupplierDalDto> mapper) : base(serviceUow, serviceUow.ProductSupplierRepository, mapper)
    {
    }

    public async Task<IEnumerable<ProductSupplierBllDto>> GetAllProductSuppliersAsync()
    {
        var res = await ServiceRepository
            .GetAllProductSuppliersAsync();
        return res.Select(x => Mapper.Map(x)!);
    }
}