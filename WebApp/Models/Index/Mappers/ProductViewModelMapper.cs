using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

public class ProductViewModelMapper
{
    public ProductMvcDto Map(ProductBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new ProductMvcDto
        {
            Id = dto.Id,
            ProductName = dto.ProductName,
            ProductDescription = dto.ProductDescription, 
            ProductPrice = dto.ProductPrice
        };
    }
    
    /*public ProductBllDto Map(ProductMvcDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new ProductBllDto
        {
            Id = dto.Id,
            ProductName = dto.ProductName,
            ProductDescription = dto.ProductDescription, 
            ProductPrice = dto.ProductPrice
        };
    }*/
}