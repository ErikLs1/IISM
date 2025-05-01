using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ProductSupplierRepository : BaseRepository<ProductSupplierDalDto, ProductSupplier>, IProductSupplierRepository
{
    public ProductSupplierRepository(AppDbContext repositoryDbContext, IUowMapper<ProductSupplierDalDto, ProductSupplier> uowMapper) : base(repositoryDbContext, uowMapper)
    {
    }
}