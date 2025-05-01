using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class ProductBllMapper : IBllMapper<ProductBllDto, ProductDalDto>
{
    public ProductDalDto? Map(ProductBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public ProductBllDto? Map(ProductDalDto? entity)
    {
        throw new NotImplementedException();
    }
}