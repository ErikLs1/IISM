using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class WarehouseRepository : BaseRepository<WarehouseDto, Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(AppDbContext repositoryDbContext, IMapper<WarehouseDto, Warehouse> mapper) : base(repositoryDbContext, mapper)
    {
    }
}