using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class ProductMapper : IMapper<ProductDto, Product>
{
    public ProductDto? Map(Product? entity)
    {
        throw new NotImplementedException();
    }

    public Product? Map(ProductDto? entity)
    {
        throw new NotImplementedException();
    }
}