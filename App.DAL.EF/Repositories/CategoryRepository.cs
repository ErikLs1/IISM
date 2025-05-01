using App.DAL.Contracts;
using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CategoryRepository : BaseRepository<CategoryDalDto, Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext repositoryDbContext, IUowMapper<CategoryDalDto, Category> uowMapper) : base(repositoryDbContext, uowMapper)
    {
    }
}