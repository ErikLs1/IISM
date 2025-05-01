using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class CategoryService : BaseService<CategoryBllDto, CategoryDalDto>, ICategoryService
{
    public CategoryService(
        IBaseUow serviceUow, 
        IBaseRepository<CategoryDalDto, Guid> serviceRepository, 
        IBllMapper<CategoryBllDto, CategoryDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}