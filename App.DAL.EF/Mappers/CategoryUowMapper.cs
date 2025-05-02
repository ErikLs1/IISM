using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class CategoryUowMapper : IUowMapper<CategoryDalDto, Category>
{

    public CategoryDalDto? Map(Category? entity)
    {
        if (entity == null) return null;

        var dto = new CategoryDalDto()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
            CategoryDescription = entity.CategoryDescription,
            Products = entity.Products == null ? [] : 
                entity.Products
                    .Select(o => new ProductDalDto()
                    {
                        Id =o.CategoryId,
                        ProductName = o.ProductName,
                        ProductDescription = o.ProductDescription,
                        ProductPrice = o.ProductPrice,
                        ProductStatus = o.ProductStatus
                    }).ToList(),
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
            entity.Products = dto.Products == null
                ? []
                : dto.Products
                    .Select(o => new Product()
                    {
                        Id =o.CategoryId,
                        ProductName = o.ProductName,
                        ProductDescription = o.ProductDescription,
                        ProductPrice = o.ProductPrice,
                        ProductStatus = o.ProductStatus
                    }).ToList();
        }

        return entity;
    }
}