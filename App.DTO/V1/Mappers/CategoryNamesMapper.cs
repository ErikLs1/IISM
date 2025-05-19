using App.BLL.DTO;
using App.DTO.V1.DTO;

namespace App.DTO.V1.Mappers;

public class CategoryNamesMapper
{
    public CategoryNamesDto? Map(CategoryNamesBllDto? entity)
    {
        if (entity == null) return null;

        var res = new CategoryNamesDto()
        {
            CategoryNames = entity.CategoryNames
        };
        return res;
    }
}