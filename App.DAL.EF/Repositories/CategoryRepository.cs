using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CategoryRepository : BaseRepository<CategoryDalDto, Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new CategoryUowMapper())
    {
    }

    public async Task<CategoryNamesDalDto> GetDistinctCategoryNamesAsync()
    {
        var query = GetQuery();
        var names = await query.Select(c => c.CategoryName).Distinct().OrderBy(x => x).ToListAsync();
        return new CategoryNamesDalDto
        {
            CategoryNames = names
        };
    }
}