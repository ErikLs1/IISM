using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

public class CategoryViewModelMapper
{
    public CategoryMvcDto Map(CategoryBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new CategoryMvcDto()
        {
            Id = dto.Id,
            CategoryName = dto.CategoryName,
            CategoryDescription = dto.CategoryDescription
        };
    }
    
    public CategoryBllDto Map(CategoryMvcDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new CategoryBllDto()
        {
            Id = dto.Id,
            CategoryName = dto.CategoryName,
            CategoryDescription = dto.CategoryDescription
        };
    }
}