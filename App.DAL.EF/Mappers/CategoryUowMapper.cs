using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class CategoryUowMapper : IUowMapper<CategoryDalDto, Category>
{
    private readonly ProductUowMapper _productUowMapper;

    public CategoryUowMapper(ProductUowMapper productUowMapper)
    {
        _productUowMapper = productUowMapper;
    }

    public CategoryDalDto? Map(Category? entity)
    {
        if (entity == null) return null;

        var dto = new CategoryDalDto()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
            CategoryDescription = entity.CategoryDescription,
            Products = entity.Products?
                .Select(o => _productUowMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public Category? Map(CategoryDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Category()
        {
            Id = dto.Id,
            CategoryName = dto.CategoryName,
            CategoryDescription = dto.CategoryDescription,
        };

        if (dto.Products != null)
        {
            entity.Products = dto.Products?
                .Select(o => _productUowMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}