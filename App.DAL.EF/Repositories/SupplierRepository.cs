using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class SupplierRepository : BaseRepository<SupplierDalDto, Supplier>, ISupplierRepository
{
    public SupplierRepository(AppDbContext repositoryDbContext, IUowMapper<SupplierDalDto, Supplier> uowMapper) : base(repositoryDbContext, uowMapper)
    {
    }
}