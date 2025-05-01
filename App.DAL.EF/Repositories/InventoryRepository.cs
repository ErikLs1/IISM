using App.DAL.Contracts;
using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InventoryRepository : BaseRepository<InventoryDalDto, Inventory>, IInventoryRepository
{
    public InventoryRepository(AppDbContext repositoryDbContext, IUowMapper<InventoryDalDto, Inventory> uowMapper) : base(repositoryDbContext, uowMapper)
    {
    }
}