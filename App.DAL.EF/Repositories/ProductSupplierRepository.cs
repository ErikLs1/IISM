using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ProductSupplierRepository : BaseRepository<ProductSupplier>, IProductSupplierRepository
{
    public ProductSupplierRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}