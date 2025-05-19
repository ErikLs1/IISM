using App.BLL.DTO;
using App.DAL.DTO;

namespace App.BLL.Mappers;

public class CategoryNamesBllMapper
{
    public CategoryNamesBllDto? Map(CategoryNamesDalDto? entity)
    {
        if (entity == null) return null;

        var res = new CategoryNamesBllDto()
        {
            CategoryNames = entity.CategoryNames
        };
        
        return res;
    }
}