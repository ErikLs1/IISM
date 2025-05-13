using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ProductSupplierRepository : BaseRepository<ProductSupplierDalDto, ProductSupplier>, IProductSupplierRepository
{
    public ProductSupplierRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ProductSupplierUowMapper())
    {
    }

    public async Task<IEnumerable<ProductSupplierDalDto>> GetAllProductSuppliersAsync()
    {
        var result = await GetQuery()
                .Include(s => s.Supplier)
                .Include(p => p.Product)
                .ToArrayAsync();
        return result.Select(x => Mapper.Map(x)!);
    }
}