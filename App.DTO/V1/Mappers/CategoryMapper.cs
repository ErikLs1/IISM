using App.BLL.DTO;
using App.DTO.V1.DTO;
using Base.Contracts;

namespace App.DTO.V1.Mappers;

public class CategoryMapper : IMapper<CategoryDto, CategoryBllDto>
{
    public CategoryDto? Map(CategoryBllDto? entity)
    {
        if (entity == null) return null;

        var res = new CategoryDto()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
            CategoryDescription = entity.CategoryDescription,
        };

        return res;
    }

    public CategoryBllDto? Map(CategoryDto? entity)
    {
        if (entity == null) return null;

        var res = new CategoryBllDto()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
            CategoryDescription = entity.CategoryDescription
        };

        return res;
    }
    

    public CategoryBllDto? Map(CategoryCreateDto? entity)
    {
        if (entity == null) return null;

        var res = new CategoryBllDto()
        {
            Id = Guid.NewGuid(),
            CategoryName = entity.CategoryName,
            CategoryDescription = entity.CategoryDescription
            
        };

        return res;
    }
}