using App.BLL.DTO;
using App.DAL.DTO;
using Base.Contracts;

namespace App.BLL.Mappers;

public class CategoryBllMapper : IMapper<CategoryBllDto, CategoryDalDto>
{
    public CategoryBllDto? Map(CategoryDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new CategoryBllDto()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
            CategoryDescription = entity.CategoryDescription,
            Products = entity.Products == null ? [] : 
                entity.Products
                    .Select(o => new ProductBllDto()
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

    public CategoryDalDto? Map(CategoryBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new CategoryDalDto()
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
                    .Select(o => new ProductDalDto()
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