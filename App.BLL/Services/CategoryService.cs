using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Services;

public class CategoryService : BaseService<CategoryBllDto, CategoryDalDto, ICategoryRepository>, ICategoryService
{
    public CategoryService(
        IAppUow serviceUow, 
        IMapper<CategoryBllDto, CategoryDalDto> mapper) : base(serviceUow, serviceUow.CategoryRepository, mapper)
    {
    }
}