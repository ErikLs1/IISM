using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class CategoryMapper : IMapper<CategoryDto, Category>
{
    public CategoryDto? Map(Category? entity)
    {
        throw new NotImplementedException();
    }

    public Category? Map(CategoryDto? entity)
    {
        throw new NotImplementedException();
    }
}