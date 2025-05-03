using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;

namespace App.BLL.Services;

public class CategoryService : BaseService<CategoryBllDto, CategoryDalDto, ICategoryRepository>, ICategoryService
{
    public CategoryService(
        IAppUow serviceUow, 
        IBllMapper<CategoryBllDto, CategoryDalDto> bllMapper) : base(serviceUow, serviceUow.CategoryRepository, bllMapper)
    {
    }
}