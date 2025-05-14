using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ProductRepository : BaseRepository<ProductDalDto, Product>, IProductRepository
{
    public ProductRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ProductUowMapper())
    {
    }

    public async Task<decimal> GetProductPriceById(Guid productId)
    {
        var price = await GetQuery()
            .Where(p => p.Id == productId)
            .Select(p => p.ProductPrice)
            .FirstOrDefaultAsync();
        return price;
    }
}