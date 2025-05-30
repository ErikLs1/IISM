using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface ICategoryRepository : IBaseRepository<CategoryDalDto>
{
    Task<CategoryNamesDalDto> GetDistinctCategoryNamesAsync();
}