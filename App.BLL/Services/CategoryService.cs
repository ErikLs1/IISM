using App.BLL.Contracts;
using App.BLL.DTO;
using App.BLL.Mappers;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class CategoryService : BaseService<CategoryBllDto, CategoryDalDto, ICategoryRepository>, ICategoryService
{
    private readonly CategoryNamesBllMapper _bllMapper = new CategoryNamesBllMapper();
    public CategoryService(
        IAppUow serviceUow, 
        IMapper<CategoryBllDto, CategoryDalDto> mapper) : base(serviceUow, serviceUow.CategoryRepository, mapper)
    {
    }

    public async Task<CategoryNamesBllDto> GetDistinctCategoryNamesAsync()
    {
        var res = await ServiceRepository.GetDistinctCategoryNamesAsync();
        return _bllMapper.Map(res)!;
    }
}