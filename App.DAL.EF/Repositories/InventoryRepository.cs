using App.DAL.Contracts;
using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InventoryRepository : BaseRepository<InventoryDto, Inventory>, IInventoryRepository
{
    public InventoryRepository(AppDbContext repositoryDbContext, IMapper<InventoryDto, Inventory> mapper) : base(repositoryDbContext, mapper)
    {
    }
}