using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class ProductService : BaseService<ProductBllDto, ProductDalDto, IProductRepository>, IProductService
{
    public ProductService(
        IAppUow ouw, 
        IMapper<ProductBllDto, ProductDalDto> mapper) : base(ouw, ouw.ProductRepository, mapper)
    {
    }
    
    
}