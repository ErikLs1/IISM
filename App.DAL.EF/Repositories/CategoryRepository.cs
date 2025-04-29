using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CategoryRepository : BaseRepository<CategoryDto, Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new CategoryMapper())
    {
    }
}