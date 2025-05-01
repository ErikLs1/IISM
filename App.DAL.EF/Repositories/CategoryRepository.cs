using App.DAL.Contracts;
using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CategoryRepository : BaseRepository<CategoryDto, Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext repositoryDbContext, IMapper<CategoryDto, Category> mapper) : base(repositoryDbContext, mapper)
    {
    }
}