using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class CategoryMapper : IMapper<CategoryDto, Category>
{
    private readonly ProductMapper _productMapper;

    public CategoryMapper(ProductMapper productMapper)
    {
        _productMapper = productMapper;
    }

    public CategoryDto? Map(Category? entity)
    {
        if (entity == null) return null;

        var dto = new CategoryDto()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
            CategoryDescription = entity.CategoryDescription,
            Products = entity.Products?
                .Select(o => _productMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public Category? Map(CategoryDto? dto)
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
                .Select(o => _productMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}