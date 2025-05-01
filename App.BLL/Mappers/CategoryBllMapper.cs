using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class CategoryBllMapper : IBllMapper<CategoryBllDto, CategoryDalDto>
{
    public CategoryDalDto? Map(CategoryBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public CategoryBllDto? Map(CategoryDalDto? entity)
    {
        throw new NotImplementedException();
    }
}