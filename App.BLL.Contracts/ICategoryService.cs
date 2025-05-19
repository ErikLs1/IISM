using App.BLL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface ICategoryService : IBaseService<CategoryBllDto>
{
    Task<CategoryNamesBllDto> GetDistinctCategoryNamesAsync();
    
}